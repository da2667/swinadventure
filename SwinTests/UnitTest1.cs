using System.ComponentModel;
using SwinAdventure;

namespace SwinTests;

public class Tests
{
    // Identifiable Object Unit Tests
    private Bag b1;
    private Bag b2;
    private Item i1;
    private Item i2;
    private Item gem;
    private Player p;
    private Inventory pi;
    private LookCommand lCmd;
  

    [SetUp]
    public void Setup()
    {
        b1 = new Bag(new string[] { "bag", "items" }, "item", "bag");
        b2 = new Bag(new string[] { "bag 2", "items" }, "item", "bag");
        i1 = new Item(new string[] { "sword", "red" }, "red sword", "red");
        i2 = new Item(new string[] { "dagger", "blue" }, "blue dagger", "blue");
        gem = new Item(new string[] { "gem", "purple" }, "purple gem", "purple");
        lCmd = new LookCommand(new string[] { });
        p = new Player("dylan", "player #1");
        pi = new Inventory();
    }
   

    [Test]
    public void TestAreYou()
    {
        IdentifiableObject id = new IdentifiableObject(new string[] { "Fred", "Bob" });

        bool AreYouFred = id.AreYou("fred");
        bool AreYouBob = id.AreYou("bob");

        Assert.Multiple(() =>
        {
            Assert.That(AreYouFred, Is.EqualTo(true));
            Assert.That(AreYouBob, Is.EqualTo(true));
        });
        
    }

    [Test]
    public void TestNotAreYou()
    {
        IdentifiableObject id = new IdentifiableObject(new string[] { "Wilma", "Boby" });

        bool AreYouFred = id.AreYou("fred");
        bool AreYouBob = id.AreYou("bob");
        Assert.Multiple(() =>
        {
            Assert.That(AreYouFred, Is.EqualTo(false));
            Assert.That(AreYouBob, Is.EqualTo(false));
        });
    }

    [Test]
    public void TestCaseSensitive()
    {
        IdentifiableObject id = new IdentifiableObject(new string[] { "Fred", "Bob" });

        bool AreYouFred = id.AreYou("FRED");
        bool AreYouBob = id.AreYou("bOB");

        Assert.Multiple(() =>
        {
            Assert.That(AreYouFred, Is.EqualTo(true));
            Assert.That(AreYouBob, Is.EqualTo(true));
        });
    }

    [Test]
    public void TestFirstId()
    {
        IdentifiableObject id = new IdentifiableObject(new string[] { "Fred", "Bob" });

        string FirstId = id.FirstId;
        Assert.That(FirstId, Is.EqualTo("fred"));
    }

    [Test]
    public void TestFirstIdNoIds()
    {
        IdentifiableObject id = new IdentifiableObject(new string[] { });

        string FirstId = id.FirstId;
        Assert.That(FirstId, Is.EqualTo(""));
    }

    [Test]
    public void TestAddId()
    {
        IdentifiableObject id = new IdentifiableObject(new string[] { "Fred", "Bob" });

        id.AddIdentifier("Wilma");

        bool AreYouFred = id.AreYou("fred");
        bool AreYouBob = id.AreYou("bob");
        bool AreYouWilma = id.AreYou("wilma");

        Assert.Multiple(() =>
        {
            Assert.That(AreYouFred, Is.EqualTo(true));
            Assert.That(AreYouBob, Is.EqualTo(true));
            Assert.That(AreYouWilma, Is.EqualTo(true));
        });
    }

    // Item Unit Tests

    [Test]
    public void TestItemIsIdentifiable()
    {

        bool ItemIsIdentifiable = i1.AreYou("sword");

        Assert.That(ItemIsIdentifiable, Is.True);
    }

    [Test]
    public void TestItemShortDescription()
    {

        string ItemShortDescription = i1.ShortDescription;

        Assert.That(ItemShortDescription, Is.EqualTo("a " + "red sword " + "(sword)"));
    }

    [Test]
    public void TestItemFullDescription()
    {
        string ItemFullDescription = i1.FullDescription;

        Assert.That(ItemFullDescription, Is.EqualTo(i1.FullDescription));
    }

    // Inventory Unit Tests

    [Test]
    public void TestFindItem()
    {
        pi.Put(i1);

        Assert.That(pi.HasItem(i1.FirstId), Is.EqualTo(true));
    }

    [Test]
    public void TestNoItemFind()
    {
        pi.Put(i2);

        Assert.That(pi.HasItem("sword"), Is.EqualTo(false));
    }

    [Test]
    public void TestFetchItem()
    {
        pi.Put(i1);

        Item FetchedItem = pi.Fetch(i1.FirstId);

        Assert.Multiple(() =>
        {
            Assert.That(FetchedItem, Is.EqualTo(i1));
            Assert.That(pi.HasItem("sword"), Is.EqualTo(true));
        });
    }

    [Test]
    public void TestTakeItem()
    {
        pi.Put(i1);

        Item TakenItem = pi.Take(i1.FirstId);

        Assert.Multiple(() =>
        {
            Assert.That(TakenItem, Is.EqualTo(i1));
            Assert.That(pi.HasItem("sword"), Is.EqualTo(false));
        });
    }

    [Test]
    public void TestItemList()
    {
        pi.Put(i1);
        pi.Put(i2);

        Assert.That(pi.ItemList, Is.EqualTo("\ta red sword (sword)\n\ta blue dagger (dagger)\n"));
    }

    // Player Unit Tests

    [Test]
    public void TestPlayerIsIdentifiable()
    {
        bool PlayerAreYouMe = p.AreYou(p.FirstId);

        Assert.That(PlayerAreYouMe, Is.EqualTo(true));
    }

    [Test]
    public void TestPlayerLocatesItems()
    {
        p.Inventory.Put(i1);
        
        GameObject LocatedInventoryItem = p.Locate(i1.FirstId);

        Assert.That(LocatedInventoryItem, Is.EqualTo(i1));
    }

    [Test]
    public void TestPlayerLocatesItself()
    {
        GameObject LocatedPlayer = p.Locate(p.FirstId);

        Assert.That(LocatedPlayer, Is.EqualTo(p));
    }

    [Test]
    public void TestPlayerLocatesNothing()
    {
        // Item hasn't been put into the player's inventory

        GameObject LocatedInventoryItem = p.Locate(i1.FirstId);

        Assert.That(LocatedInventoryItem, Is.EqualTo(null));
    }

    [Test]
    public void TestPlayerFullDescription()
    {
        string PlayerFullDescription = p.FullDescription;

        Assert.That(PlayerFullDescription, Is.EqualTo(p.FullDescription));
    }

    // Bag Unit Tests

    [Test]
    public void TestBagLocatesItems()
    {
        b1.Inventory.Put(i1);

        GameObject LocatedItem = b1.Locate(i1.FirstId);

        Assert.That(LocatedItem, Is.EqualTo(i1));
    }

    [Test]
    public void TestBagLocatesItself()
    {
        GameObject LocatedItem = b1.Locate(b1.FirstId);

        Assert.That(LocatedItem, Is.EqualTo(b1));
    }

    [Test]
    public void TestBagLocatesNothing()
    {
        GameObject LocatedItem = b1.Locate(i1.FirstId);

        Assert.That(LocatedItem, Is.EqualTo(null));
    }

    [Test]
    public void TestBagFullDescription()
    {
        string FullDescription = b1.FullDescription;

        Assert.That(b1.FullDescription, Is.EqualTo(FullDescription));
    }

    [Test]
    public void TestBagInBag()
    {
        b1.Inventory.Put(i1);
        b1.Inventory.Put(b2);
        b2.Inventory.Put(i2);

        GameObject LocateB2 = b1.Locate(b2.FirstId);
        GameObject LocateItemB1 = b1.Locate(i1.FirstId);
        GameObject LocateItemB2 = b1.Locate(i2.FirstId);

        Assert.Multiple(() =>
        {
            Assert.That(LocateB2, Is.EqualTo(b2));
            Assert.That(LocateItemB1, Is.EqualTo(i1));
            Assert.That(LocateItemB2, Is.EqualTo(null));
        });

    }

    // Look Command Unit Tests

    [Test]
    public void TestLookAtMe()
    {
        string playerInventory = lCmd.Execute(p, new string[]{ "look", "at", "inventory" });

        Assert.That(playerInventory, Is.EqualTo(p.FullDescription));
    }

    [Test]
    public void TestLookAtGem()
    {
        p.Inventory.Put(gem);
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "inventory" });

        Assert.That(gemDescription, Is.EqualTo(gem.FullDescription));
    }

    [Test]
    public void TestLookAtUnk()
    {
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "inventory" });

        Assert.That(gemDescription, Is.EqualTo("I can't find the gem"));
    }

    [Test]
    public void TestLookAtGemInMe()
    {
        // Isn't this the same test?? - It's in the instructions...
        p.Inventory.Put(gem);
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "inventory" });

        Assert.That(gemDescription, Is.EqualTo(gem.FullDescription));
    }

    [Test]
    public void TestLookAtGemInBag()
    {
        b1.Inventory.Put(gem);
        p.Inventory.Put(b1);
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "bag" });

        Assert.That(gemDescription, Is.EqualTo(gem.FullDescription));
    }
    [Test]
    public void TestLookAtGemInNoBag()
    {
        b1.Inventory.Put(gem);
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "bag" });

        Assert.That(gemDescription, Is.EqualTo("I can't find the bag"));
    }

    [Test]
    public void TestLookAtNoGemInBag()
    {
        p.Inventory.Put(b1);
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "bag" });

        Assert.That(gemDescription, Is.EqualTo("I can't find the gem"));
    }

    [Test]
    public void TestInvalidLook()
    {
        string cmd1 = lCmd.Execute(p, new string[] { "look", "it's", "a", "bird!!!!" });
        string cmd2 = lCmd.Execute(p, new string[] { "stop", "hammer", "time!>>>$" });
        string cmd3 = lCmd.Execute(p, new string[] { "look", "at", "x", "and", "y" });
        string cmd4 = lCmd.Execute(p, new string[] { "look", "at", "noel", "and", "liam", "gallagher" });
        
        Assert.Multiple(() =>
        {
            Assert.That(cmd1, Is.EqualTo("What do you want to look at?"));
            Assert.That(cmd2, Is.EqualTo("Error in look input"));
            Assert.That(cmd3, Is.EqualTo("I can't find the y"));
            Assert.That(cmd4, Is.EqualTo("I don't know how to look like that"));
        });
    }
}
