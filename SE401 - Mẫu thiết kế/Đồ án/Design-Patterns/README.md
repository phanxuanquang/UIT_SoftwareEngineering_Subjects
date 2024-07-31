## © 2023 University of Information Technology / Design Pattern
_________________________________
# Overview

This repository aims to provide a comprehensive collection of design patterns along with their explanations and code examples in various programming languages. Whether you're a beginner or an experienced developer, this repository will serve as a valuable resource to enhance your understanding of software design principles. It contains code examples and explanations of various design patterns that are commonly used in software engineering. Design patterns are general, reusable solutions to recurring problems in software design. They are not specific code, but rather templates or guidelines for how to solve a problem in different situations.

![alt text](https://i.imgur.com/Z7WQvq0.png)

Design patterns are reusable solutions to common problems that arise during software design and development. They provide proven solutions and best practices that help in creating flexible, maintainable, and scalable software systems. By leveraging design patterns, developers can enhance code reusability, modularity, and extensibility, resulting in more efficient and robust applications.

Documentation for Vietnamese people can be found [HERE](https://github.com/phanxuanquang/Design-Patterns/blob/master/Documentation.pdf)

Please be noted that the explanations and code examples in this repository are for educational purposes only and may not cover every possible implementation scenario. It's recommended to adapt and modify the patterns to fit your specific project requirements.
<details>
  <summary>Creational Patterns</summary>

  
Creational patterns are concerned with how objects are created and initialized. They can help you control the complexity and variability of object creation, and encapsulate the logic and details of object creation from the rest of the system.

Some examples of creational patterns are:

•  Singleton: Ensures that only one instance of a class exists and provides a global access point to it.

•  Factory Method: Defines an interface for creating an object, but lets subclasses decide which class to instantiate.

•  Abstract Factory: Provides an interface for creating families of related or dependent objects without specifying their concrete classes.

•  Builder: Separates the construction of a complex object from its representation, allowing the same construction process to create different representations.

•  Prototype: Specifies the kinds of objects to create using a prototypical instance, and creates new objects by copying this prototype.
</details>
<details>
  <summary>Structural Patterns</summary>
  
Structural patterns are concerned with how classes and objects are composed and organized. They can help you define the relationships and dependencies between different components, and facilitate the communication and cooperation between them.

Some examples of structural patterns are:

•  Adapter: Allows classes with incompatible interfaces to work together by wrapping its own interface around that of an existing class.

•  Bridge: Decouples an abstraction from its implementation, allowing them to vary independently.

•  Composite: Composes objects into tree structures to represent part-whole hierarchies, and lets clients treat individual objects and compositions uniformly.

•  Decorator: Attaches additional responsibilities to an object dynamically, providing a flexible alternative to subclassing for extending functionality.

•  Facade: Provides a unified interface to a set of interfaces in a subsystem, defining a higher-level interface that makes the subsystem easier to use.

•  Flyweight: Uses sharing to support large numbers of fine-grained objects efficiently, reducing the memory and resource consumption.

•  Proxy: Provides a surrogate or placeholder for another object to control access to it, adding a layer of indirection or protection.

</details>

<details>
    <summary>Behavioral Patterns</summary>
  
Behavioral patterns are concerned with how classes and objects interact and distribute responsibilities. They can help you define the communication protocols between objects, and encapsulate the algorithms and logic behind them.

Some examples of behavioral patterns are:

•  Chain of Responsibility: Avoids coupling the sender of a request to its receiver by giving more than one object a chance to handle the request, creating a chain of potential handlers.

•  Command: Encapsulates a request as an object, allowing you to parameterize clients with different requests, queue or log requests, and support undoable operations.

•  Interpreter: Given a language, defines a representation for its grammar along with an interpreter that uses the representation to interpret sentences in the language.

•  Iterator: Provides a way to access the elements of an aggregate object sequentially without exposing its underlying representation.

•  Mediator: Defines an object that encapsulates how a set of objects interact, promoting loose coupling by keeping objects from referring to each other explicitly.

•  Memento: Without violating encapsulation, captures and externalizes an object's internal state so that the object can be restored to this state later.

•  Observer: Defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

•  State: Allows an object to alter its behavior when its internal state changes, making it appear as if the object changed its class.

•  Strategy: Defines a family of algorithms, encapsulates each one, and makes them interchangeable, letting the algorithm vary independently from clients that use it.

•  Template Method: Defines the skeleton of an algorithm in an operation, deferring some steps to subclasses, letting subclasses redefine certain steps of an algorithm without changing its structure.

•  Visitor: Represents an operation to be performed on the elements of an object structure, letting you define a new operation without changing the classes of the elements on which it operates.
</details>

# Contribution Guidelines
Contributions to this repository are highly encouraged and appreciated! If you have additional design patterns to share, code improvements, or bug fixes, please follow these guidelines:
1 - Fork the repository and create a new branch for your contribution.

2 - Make your changes or additions, ensuring that the code is well-documented and adheres to the repository's structure.

3 - Test your changes thoroughly to ensure they do not introduce any regressions.

4 - Submit a pull request with a clear description of your changes, the problem it solves, and any additional information that might be helpful for review.

# Feedback and Support

If you have any questions, suggestions, or need assistance with the repository, feel free to open an issue or contact the repository maintainers.

We hope this repository proves to be a valuable resource in your software development journey. Happy coding!


