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

## Lab 3 - Corporate Messaging System

This project implements a corporate message distribution system to practice *OOP principles, GRASP, SOLID, structural and creational design patterns, mocking*.

### Key Features:
- **Core entities**:
  - **Message** – title, body, importance level.
  - **Topic** – name, list of recipients, sends messages to all recipients.
  - **Recipient types**:
    - **User** – corporate user with a message inbox and message status tracking (read/unread).
    - **Messenger** – sends messages to an external messenger.
    - **Display** – outputs messages to a physical display (supports console and file output).
    - **Group** – composite recipient that forwards messages to multiple recipients.
- **Message handling**:
  - Filters per recipient based on importance level.
  - Logging of incoming messages with testable mock implementations.
- **User logic**:
  - Track message status (read/unread).
  - Change status only if the message is unread; invalid actions return an error.
- **Display driver**:
  - Supports clearing display, setting text color, and writing text output.
- **Testability**:
  - Messenger and display implementations are isolated, mockable, and have no direct dependency on message delivery logic.
  - Structural patterns ensure clean integrations with external systems.
- **Unit Tests**:
  - Check message status changes and invalid transitions.
  - Verify filtering, logging, and correct messenger/display behavior.
  - Handle multiple recipients with importance filters.

## Lab 4 - File System Manager

This project implements a console-based file system manager to practice *SOLID principles and design patterns (behavioral, structural, and creational)*.

### Key Features:
- File system operations: navigate (absolute/relative paths), view directory and file contents, move, copy, delete, and rename files.
- Console interface with command flags and extensible command parsing.
- Local file system integration, with logic decoupled from console handling and file system specifics.
- Configurable directory tree output with adjustable depth and symbols.
- Support for multiple file systems (e.g., switching drives).
- Collision handling for file names.
- Unit Tests: validate the command parser to ensure commands create correct objects with correct arguments.

### Command Semantics
- **connect [Address] [-m Mode]**
  - Address – Absolute path to the file system being connected.
  - Mode – File system mode (only local mode local is required to be implemented).
- **disconnect**
  - Disconnects from the file system.
- **tree goto [Path]**
  - Path – Relative or absolute path to the directory in the file system.
- **tree list {-d Depth}**
  - Depth – Parameter that specifies the depth of the directory tree; must be specified with the -d flag.
- **file show [Path] {-m Mode}**
  - Path – Relative or absolute path to the file.
  - Mode – File output mode (only console output mode console is required to be implemented).
- **file move [SourcePath] [DestinationPath]**
  - SourcePath – Relative or absolute path to the file to be moved.
  - DestinationPath – Relative or absolute path to the directory where the file should be moved.
- **file copy [SourcePath] [DestinationPath]**
  - SourcePath – Relative or absolute path to the file to be copied.
  - DestinationPath – Relative or absolute path to the directory where the file should be copied.
- **file delete [Path]**
  - Path – Relative or absolute path to the file to be deleted.
- **file rename [Path] [Name]**
  - Path – Relative or absolute path to the file to be renamed.
  - Name – New name for the file.

