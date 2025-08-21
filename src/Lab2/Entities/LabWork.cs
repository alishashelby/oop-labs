using System;
using Itmo.ObjectOrientedProgramming.Lab2.Prototype;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class LabWork : IEntity, IPrototype<LabWork>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LabWork"/> class.
    /// </summary>
    /// <param name="title">The title of LabWork to write.</param>
    /// <param name="description">The description of LabWork to write.</param>
    /// <param name="evaluationCriteria">The evaluation criteria of LabWork to write.</param>
    /// <param name="points">The points of LabWork to write.</param>
    /// <param name="author">The author of LabWork to write.</param>
    private LabWork(string title, string description, string evaluationCriteria, int points, User author)
    {
        this.Id = Guid.NewGuid();
        this.Title = title;
        this.Description = description;
        this.EvaluationCriteria = evaluationCriteria;
        this.Points = points;
        this.Author = author;
        this.BaseLabId = null;
    }

    public static LabWorkBuilder Builder => new LabWorkBuilder();

    public Guid Id { get; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string EvaluationCriteria { get; private set; }

    public int Points { get; private set; }

    public User Author { get; }

    public Guid? BaseLabId { get; private set; }

    /// <summary>
    /// Updates name of labWork.
    /// </summary>
    /// <param name="newTitle">The new name which should be set.</param>
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
    /// Updates description of labWork.
    /// </summary>
    /// <param name="newDescription">The new description which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateDescription(string newDescription, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.Description = newDescription;
        return true;
    }

    /// <summary>
    /// Updates description of labWork.
    /// </summary>
    /// <param name="newEvaluationCriteria">The new evaluation criteria which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateEvaluationCriteria(string newEvaluationCriteria, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.EvaluationCriteria = newEvaluationCriteria;
        return true;
    }

    /// <summary>
    /// Updates description of labWork.
    /// </summary>
    /// <param name="newPoints">The new evaluation criteria which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdatePoints(int newPoints, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.Points = newPoints;
        return true;
    }

    /// <summary>
    /// Clones entity.
    /// </summary>
    /// <returns>The LabWork object.</returns>
    public LabWork Clone()
    {
        var newLab = new LabWork(this.Title, this.Description, this.EvaluationCriteria, this.Points, this.Author)
        {
            BaseLabId = this.Id,
        };
        return newLab;
    }

    public class LabWorkBuilder
    {
        private User? _author;

        private string? _title;

        private string? _description;

        private string? _evaluationCriteria;

        private int _points;

        public LabWorkBuilder SetAuthor(User name)
        {
            _author = name;
            return this;
        }

        public LabWorkBuilder SetTitle(string name)
        {
            this._title = name;
            return this;
        }

        public LabWorkBuilder SetDescription(string descr)
        {
            this._description = descr;
            return this;
        }

        public LabWorkBuilder SetEvaluationCriteria(string eCriteria)
        {
            this._evaluationCriteria = eCriteria;
            return this;
        }

        public LabWorkBuilder SetPoints(int score)
        {
            this._points = score;
            return this;
        }

        /// <summary>
        /// Builds the LabWork.
        /// </summary>
        /// <returns>A LabWork.</returns>
        public LabWork Build()
        {
            if (_author == null)
            {
                throw new InvalidOperationException("Author must be provided.");
            }

            if (string.IsNullOrEmpty(this._title))
            {
                throw new InvalidOperationException("Title must be provided.");
            }

            if (string.IsNullOrEmpty(this._description))
            {
                throw new InvalidOperationException("Description must be provided.");
            }

            if (string.IsNullOrEmpty(this._evaluationCriteria))
            {
                throw new InvalidOperationException("Evaluation Criteria must be provided.");
            }

            return this._points <= 0
                ? throw new InvalidOperationException("Points must be positive.")
                : new LabWork(this._title, this._description, this._evaluationCriteria, this._points, this._author);
        }
    }
}