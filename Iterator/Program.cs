// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace Iterator;

internal class Program
{
    private static void Main()
    {
        var library = new Library();
        var reader = new Reader();
        reader.SeeBooks(library);

        Console.Read();
    }
}

internal class Reader
{
    public void SeeBooks(Library library)
    {
        var iterator = library.CreateNumerator();
        while (iterator.HasNext())
        {
            var book = iterator.Next();
            Console.WriteLine(book.Name);
        }
    }
}

internal interface IBookIterator
{
    bool HasNext();

    Book Next();
}

internal interface IBookNumerable
{
    IBookIterator CreateNumerator();

    int Count { get; }

    Book this[int index] { get; }
}

internal class Book
{
    public string Name { get; set; } = null!;
}

internal class Library : IBookNumerable
{
    private readonly Book[] books;

    public Library()
    {
        this.books = new Book[]
        {
            new Book
            {
                Name = "Война и мир",
            },
            new Book
            {
                Name = "Отцы и дети",
            },
            new Book
            {
                Name = "Вишневый сад",
            },
        };
    }

    public int Count => this.books.Length;

    public Book this[int index] => this.books[index];

    public IBookIterator CreateNumerator() { return new LibraryNumerator(this); }
}

internal class LibraryNumerator : IBookIterator
{
    private readonly IBookNumerable aggregate;

    private int index = 0;

    public LibraryNumerator(IBookNumerable a) { this.aggregate = a; }

    public bool HasNext() { return this.index < this.aggregate.Count; }

    public Book Next() { return this.aggregate[this.index++]; }
}
