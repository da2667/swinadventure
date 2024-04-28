using System;

namespace SwinAdventure;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your player's name?\n>");
        string? playerName = Console.ReadLine();

        Console.Write("What is your player description?\n>");
        string? playerDesc = Console.ReadLine();

        Player p = new Player(playerName, playerDesc);

        Item i1 = new Item(new string[] { "sword", "red" }, "sword", "A red sword of the deadliest power");
        Item i2 = new Item(new string[] { "dagger", "blue" }, "dagger", "This is a blue dagger");

        Item i3 = new Item(new string[] { "gem", "purple" }, "gem", "This is a purple gem");
        Bag b = new Bag(new string[] { "bag", "items" }, "bag", "A bag of doom");

        p.Inventory.Put(i1);
        p.Inventory.Put(i2);


        b.Inventory.Put(i3);
        p.Inventory.Put(b);

        LookCommand lCmd = new LookCommand(new string[] { });

        do
        {
            Console.Write(">");
            string cmdInput = Console.ReadLine();

            Console.WriteLine(lCmd.Execute(p, cmdInput.Split(" ")));
        } while (true);
    }
}

