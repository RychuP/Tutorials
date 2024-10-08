﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorials.Collections;

static class LinkedListExamples
{
    public static void Test()
    {
        string[] words = { "the", "fox", "jumps", "over", "the", "dog" };
        LinkedList<string> sentence = new(words);
        Display(sentence, "Sentence is:");

        var query = from word in sentence
                    where word[0] == 't'
                    select word;
        string text = string.Join(" ", query);
        Display(text, "Test 0: Extract words beginning with 't':");

        // Add the word 'today' to the beginning of the linked list.
        sentence.AddFirst("today");
        Display(sentence, "Test 1: Add 'today' to beginning of the list:");

        // Move the first node to be the last node.
        var mark1 = sentence.First;
        sentence.RemoveFirst();
        if (mark1 is not null) sentence.AddLast(mark1);
        Display(sentence, "Test 2: Move first node to be last node:");

        // Change the last node to 'yesterday'.
        sentence.RemoveLast();
        sentence.AddLast("yesterday");
        Display(sentence, "Test 3: Change the last node to 'yesterday':");

        // Move the last node to be the first node.
        mark1 = sentence.Last;
        sentence.RemoveLast();
        if (mark1 is not null) sentence.AddFirst(mark1);
        Display(sentence, "Test 4: Move last node to be first node:");

        // Indicate the last occurence of 'the'.
        sentence.RemoveFirst();
        LinkedListNode<string> current = sentence.FindLast("the");
        IndicateNode(current, "Test 5: Indicate last occurence of 'the':");

        // Add 'lazy' and 'old' after 'the' (the LinkedListNode named current).
        sentence.AddAfter(current, "old");
        sentence.AddAfter(current, "lazy");
        IndicateNode(current, "Test 6: Add 'lazy' and 'old' after 'the':");

        // Indicate 'fox' node.
        current = sentence.Find("fox");
        IndicateNode(current, "Test 7: Indicate the 'fox' node:");

        // Add 'quick' and 'brown' before 'fox':
        sentence.AddBefore(current, "quick");
        sentence.AddBefore(current, "brown");
        IndicateNode(current, "Test 8: Add 'quick' and 'brown' before 'fox':");

        // Keep a reference to the current node, 'fox',
        // and to the previous node in the list. Indicate the 'dog' node.
        mark1 = current;
        LinkedListNode<string> mark2 = current.Previous;
        current = sentence.Find("dog");
        IndicateNode(current, "Test 9: Indicate the 'dog' node:");

        // The AddBefore method throws an InvalidOperationException
        // if you try to add a node that already belongs to a list.
        Console.WriteLine("Test 10: Throw exception by adding node (fox) already in the list:");
        try
        {
            sentence.AddBefore(current, mark1);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Exception message: {0}\n", ex.Message);
        }

        // Remove the node referred to by mark1, and then add it
        // before the node referred to by current.
        // Indicate the node referred to by current.
        sentence.Remove(mark1);
        sentence.AddBefore(current, mark1);
        IndicateNode(current, "Test 11: Move a referenced node (fox) before the current node (dog):");

        // Remove the node referred to by current.
        sentence.Remove(current);
        IndicateNode(current, "Test 12: Remove current node (dog) and attempt to indicate it:");

        // Add the node after the node referred to by mark2.
        sentence.AddAfter(mark2, current);
        IndicateNode(current, "Test 13: Add node removed in test 11 after a referenced node (brown):");

        // The Remove method finds and removes the
        // first node that that has the specified value.
        sentence.Remove("old");
        Display(sentence, "Test 14: Remove node that has the value 'old':");

        // When the linked list is cast to ICollection(Of String),
        // the Add method adds a node to the end of the list.
        sentence.RemoveLast();
        ICollection<string> icoll = sentence;
        icoll.Add("rhinoceros");
        Display(sentence, "Test 15: Remove last node, cast to ICollection, and add 'rhinoceros':");

        // Create an array with the same number of elements as the linked list.
        string[] sArray = new string[sentence.Count];
        sentence.CopyTo(sArray, 0);
        text = string.Join(" ", sArray);
        Display(text, "Test 16: Copy the list to an array:");

        // Release all the nodes.
        sentence.Clear();
        text = $"list contains 'jumps' = {sentence.Contains("jumps")}";
        Display(text, "Test 17: Clear linked list.");
    }

    static void Display(string sentence, string title)
    {
        sentence = char.ToUpper(sentence[0]) + sentence.Substring(1).TrimEnd() + ".";
        Console.WriteLine(title);
        Console.WriteLine(sentence);
        Console.WriteLine();
    }

    static void Display(LinkedList<string> node, string title)
    {
        string sentence = string.Join(" ", node);
        Display(sentence, title);
    }

    // indicates a particular node in the linked list
    static void IndicateNode(LinkedListNode<string> node, string title)
    {
        StringBuilder sb = new();
        LinkedListNode<string>? tempNode;

        if (node.List is null)
            sb.Append($"node '{node.Value}' is not in the list");
        else
        {
            // indicate node
            sb.Append($"({node.Value})");

            // insert previous nodes
            tempNode = node.Previous;
            while (tempNode != null)
            {
                sb.Insert(0, tempNode.Value + " ");
                tempNode = tempNode.Previous;
            }

            // append next nodes
            tempNode = node.Next;
            while (tempNode != null)
            {
                sb.Append(" " + tempNode.Value);
                tempNode = tempNode.Next;
            }
        }

        Display(sb.ToString(), title);
    }
}