using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

/// <summary>
/// Represents the EducationalProgram.
/// </summary>
public class EducationalProgram : IEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EducationalProgram"/> class.
    /// </summary>
    /// <param name="name">The name of EducationalProgram to write.</param>
    /// <param name="programManager">The program manager of EducationalProgram to write.</param>
    /// <param name="subjectsBySemester">The subjects of EducationalProgram being mapped by semester.</param>
    private EducationalProgram(string name, User programManager, Dictionary<int, ICollection<ISubject>> subjectsBySemester)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.ProgramManager = programManager;
        this.SubjectsBySemester = subjectsBySemester;
    }

    public static EducationalProgramBuilder Builder => new EducationalProgramBuilder();

    public Guid Id { get; }

    public string Name { get; }

    public Dictionary<int, ICollection<ISubject>> SubjectsBySemester { get; }

    public User ProgramManager { get; }

    public class EducationalProgramBuilder
    {
        private readonly Dictionary<int, ICollection<ISubject>> _subjectsBySemester = [];

        private string? _name;

        private User? _programManager;

        public EducationalProgramBuilder SetName(string name)
        {
            this._name = name;
            return this;
        }

        public EducationalProgramBuilder SetProgramManager(User programManager)
        {
            this._programManager = programManager;
            return this;
        }

        /// <summary>
        /// Updates name of labWork.
        /// </summary>
        /// <param name="semester">The semester which is selected.</param>
        /// <param name="subject">The subject which should refer to this semester.</param>
        public EducationalProgramBuilder AddSubject(int semester, ISubject subject)
        {
            if (!this._subjectsBySemester.ContainsKey(semester))
            {
                this._subjectsBySemester[semester] = new List<ISubject>();
            }

            this._subjectsBySemester[semester].Add(subject);
            return this;
        }

        /// <summary>
        /// Builds the LabWork.
        /// </summary>
        /// <returns>A LabWork.</returns>
        public EducationalProgram Build()
        {
            if (this._programManager == null)
            {
                throw new InvalidOperationException("Program manager must be provided.");
            }

            if (string.IsNullOrEmpty(this._name))
            {
                throw new InvalidOperationException("Name must be provided.");
            }

            return new EducationalProgram(this._name, this._programManager, this._subjectsBySemester);
        }
    }
}