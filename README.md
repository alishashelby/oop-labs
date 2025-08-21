# oop-labs: 3 sem

## Lab 1 - Train Route Simulator

This project implements a train route simulator to practice OOP principles: *encapsulation, composition, and polymorphism*.

### Key Features:
- Train model with mass, speed, acceleration, max force, and calculation of travel time for a given distance using iterative physics-based simulation.
- **Route sections**:
    - **Regular magnetic tracks** – trains travel a fixed distance.
    - **Powered tracks** – apply positive or negative force along the path.
    - **Stations** – handle stop/start operations with speed limits and passenger boarding times.
- Route logic that simulates passing through multiple sections and validates successful completion, including safe stop at the end.
- No exceptions for business logic failures – results indicate success or failure with total travel time.
- Unit tests cover core logic.

## Lab 2 - Educational Program Constructor

This project implements a constructor for educational programs to practice *OOP principles, SOLID, GRASP, and creational design patterns*.

### Key Features:
- **Domain model** with:
  - **Users** – identified authors for all entities.
  - **Labworks** – ID, name, description, grading criteria, points, and author. Can be cloned; only the author can edit; points are immutable.
  - **Lecture materials** – ID, title, summary, content, and author. Can be cloned; only the author can edit.
  - **Subjects** – ID, name, list of labs, list of lectures, exam or pass/fail grading. Validation ensures total points = 100. Cloning allowed but with editing restrictions.
  - **Educational programs** – ID, name, list of subjects assigned to semesters, and a program manager.
- **Repositories**: In-memory storage for entities with ID-based search.
- **Validation rules**:
  - Editing by non-authors returns an error.
  - Cloned entities keep a reference to the source ID.
  - Subjects must total exactly 100 points.
- Unit tests cover cloning, validation, and repository logic.
