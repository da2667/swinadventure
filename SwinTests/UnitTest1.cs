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
        b1 = new Bag(new string[] { "bag", "items" }, "bag", "A bag of doom");
        b2 = new Bag(new string[] { "bag 2", "items" }, "2nd bag", "A second bag of somewhat doom");
        i1 = new Item(new string[] { "sword", "red" }, "sword", "A red sword of the deadliest power");
        i2 = new Item(new string[] { "dagger", "blue" }, "dagger", "This is a blue dagger");
        gem = new Item(new string[] { "gem", "purple" }, "gem", "This is a purple gem");
        lCmd = new LookCommand(new string[] { });
        p = new Player("Dylan", "A cool OOP student");
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

        Assert.That(ItemShortDescription, Is.EqualTo("a sword (sword)"));
    }

    [Test]
    public void TestItemFullDescription()
    {
        string ItemFullDescription = i1.FullDescription;

        Assert.That(ItemFullDescription, Is.EqualTo("A red sword of the deadliest power"));
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

        Assert.That(pi.ItemList, Is.EqualTo("\ta sword (sword)\r\n\ta dagger (dagger)\r\n"));
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

        Assert.That(PlayerFullDescription, Is.EqualTo("You are Dylan, A cool OOP student. You are carrying: " + p.Inventory.ItemList));
    }

    // Bag Unit Tests

    [Test]
    public void TestBagLocatesItems()
    {
        b1.Inventory.Put(i1);

        GameObject LocatedItem = b1.Locate(i1.FirstId);
        Item ItemInBagInventory = b1.Inventory.Fetch(i1.FirstId);

        Assert.Multiple(() =>
        {
            Assert.That(ItemInBagInventory, Is.EqualTo(i1));
            Assert.That(LocatedItem, Is.EqualTo(i1));
        });
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
        b1.Inventory.Put(i1);
        b1.Inventory.Put(i2);

        string FullDescription = b1.FullDescription;

        Assert.That(FullDescription, Is.EqualTo("In the bag you can see: " + b1.Inventory.ItemList));
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

        Assert.That(playerInventory, Is.EqualTo("You are Dylan, A cool OOP student. You are carrying: " + p.Inventory.ItemList));
    }

    [Test]
    public void TestLookAtGem()
    {
        p.Inventory.Put(gem);
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "inventory" });

        Assert.That(gemDescription, Is.EqualTo("This is a purple gem"));
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

        Assert.That(gemDescription, Is.EqualTo("This is a purple gem"));
    }

    [Test]
    public void TestLookAtGemInBag()
    {
        b1.Inventory.Put(gem);
        p.Inventory.Put(b1);
        string gemDescription = lCmd.Execute(p, new string[] { "look", "at", "gem", "in", "bag" });

        Assert.That(gemDescription, Is.EqualTo("This is a purple gem"));
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
