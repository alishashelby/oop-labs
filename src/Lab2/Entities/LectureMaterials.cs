using System;
using Itmo.ObjectOrientedProgramming.Lab2.Prototype;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class LectureMaterials : IEntity, IPrototype<LectureMaterials>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LectureMaterials"/> class.
    /// </summary>
    /// <param name="title">The title of LectureMaterials to write.</param>
    /// <param name="shortDescription">The short description of LectureMaterials to write.</param>
    /// <param name="content">The content of LectureMaterials to write.</param>
    /// <param name="author">The author of LectureMaterials to write.</param>
    private LectureMaterials(string title, string shortDescription, string content, User author)
    {
        this.Id = Guid.NewGuid();
        this.Title = title;
        this.ShortDescription = shortDescription;
        this.Content = content;
        this.Author = author;
    }

    public static LectureMaterialsBuilder Builder => new LectureMaterialsBuilder();

    public Guid Id { get; }

    public string Title { get; private set; }

    public string ShortDescription { get; private set; }

    public string Content { get; private set; }

    public User Author { get; }

    public Guid? BaseLectureMaterialsId { get; private set; }

    /// <summary>
    /// Updates name of labWork.
    /// </summary>
    /// <param name="newTitle">The new title which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateTitle(string newTitle, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.Title = newTitle;
        return true;
    }

    /// <summary>
    /// Updates name of labWork.
    /// </summary>
    /// <param name="newShortDescription">The new short description which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateShortDescription(string newShortDescription, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.ShortDescription = newShortDescription;
        return true;
    }

    /// <summary>
    /// Updates name of labWork.
    /// </summary>
    /// <param name="newContent">The new content which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateContent(string newContent, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.Content = newContent;
        return true;
    }

    /// <summary>
    /// Clones entity.
    /// </summary>
    /// <returns>The Lecture Materials object.</returns>
    public LectureMaterials Clone()
    {
        var newLectureMaterials = new LectureMaterials(this.Title, this.ShortDescription, this.Content, this.Author)
        {
            BaseLectureMaterialsId = this.Id,
        };
        return newLectureMaterials;
    }

    public class LectureMaterialsBuilder
    {
        private User? _author;

        private string? _title;

        private string? _shortDescription;

        private string? _content;

        public LectureMaterialsBuilder SetAuthor(User name)
        {
            _author = name;
            return this;
        }

        public LectureMaterialsBuilder SetTitle(string name)
        {
            this._title = name;
            return this;
        }

        public LectureMaterialsBuilder SetShortDescription(string descr)
        {
            this._shortDescription = descr;
            return this;
        }

        public LectureMaterialsBuilder SetContent(string con)
        {
            this._content = con;
            return this;
        }

        public LectureMaterials Build()
        {
            if (_author == null)
            {
                throw new InvalidOperationException("Author must be provided.");
            }

            if (string.IsNullOrEmpty(this._title))
            {
                throw new InvalidOperationException("Title must be provided.");
            }

            if (string.IsNullOrEmpty(this._shortDescription))
            {
                throw new InvalidOperationException("Short description must be provided.");
            }

            return string.IsNullOrEmpty(this._content)
                ? throw new InvalidOperationException("Content must be provided.")
                : new LectureMaterials(this._title, this._shortDescription, this._content, this._author);
        }
    }
}