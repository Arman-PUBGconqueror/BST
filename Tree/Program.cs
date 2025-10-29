using System;
using System.Collections.Generic;

class Node
{
    public int Value;
    public Node? Left, Right;

    public Node(int val)
    {
        Value = val;
        Left = Right = null;
    }
}

class Tree
{
    public Node? Root;

    public void Add(int val)
    {
        Root = AddRecursive(Root, val);
    }

    private Node AddRecursive(Node? node, int val)
    {
        if (node == null)
            return new Node(val);

        if (val < node.Value)
            node.Left = AddRecursive(node.Left, val);
        else
            node.Right = AddRecursive(node.Right, val);

        return node;
    }

    // Search for a value in the tree
    public bool Search(int val)
    {
        return SearchRecursive(Root, val);
    }

    private bool SearchRecursive(Node? node, int val)
    {
        if (node == null) return false;
        if (val == node.Value) return true;
        return val < node.Value ? SearchRecursive(node.Left, val) : SearchRecursive(node.Right, val);
    }

    // Remove a value from the tree
    public void Remove(int val)
    {
        Root = RemoveRecursive(Root, val);
    }

    private Node? RemoveRecursive(Node? node, int val)
    {
        if (node == null) return null;

        if (val < node.Value)
        {
            node.Left = RemoveRecursive(node.Left, val);
        }
        else if (val > node.Value)
        {
            node.Right = RemoveRecursive(node.Right, val);
        }
        else
        {
            // Node to remove found
            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            // Node with two children: replace with inorder successor (smallest in right subtree)
            Node successor = MinNode(node.Right);
            node.Value = successor.Value;
            node.Right = RemoveRecursive(node.Right, successor.Value);
        }

        return node;
    }

    private Node MinNode(Node node)
    {
        while (node.Left != null) node = node.Left;
        return node;
    }

    // Height of the tree (number of nodes on the longest path)
    public int Height()
    {
        return HeightRecursive(Root);
    }

    private int HeightRecursive(Node? node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(HeightRecursive(node.Left), HeightRecursive(node.Right));
    }

    // In-order print (ascending)
    public void Print()
    {
        if (Root is not null)
        {
            PrintRecursive(Root);
        }
        Console.WriteLine();
    }

    private void PrintRecursive(Node? node)
    {
        if (node == null) return;
        PrintRecursive(node.Left);
        Console.Write(node.Value + " ");
        PrintRecursive(node.Right);
    }
}

class Program
{
    static void Main()
    {
        Tree t = new Tree();
        t.Add(10);
        t.Add(5);
        t.Add(15);
        t.Add(3);
        t.Add(7);
        Console.Write("In-order (Print): ");
        t.Print(); // Output: 3 5 7 10 15

        Console.WriteLine($"Search 7: {t.Search(7)}");
        Console.WriteLine($"Search 12: {t.Search(12)}");

        Console.WriteLine($"Height: {t.Height()}");

        // Remove a leaf, node with one child, and node with two children
        t.Remove(3); // leaf
        Console.Write("After removing 3: "); t.Print();

        t.Remove(5); // node with one child (7)
        Console.Write("After removing 5: "); t.Print();

        t.Remove(10); // root (has two children)
        Console.Write("After removing 10 (root): "); t.Print();
    }
}