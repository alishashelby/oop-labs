# oop-labs

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
