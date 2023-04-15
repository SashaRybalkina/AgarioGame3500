```
Author:     Aurora Zuo
Partner:    Sasha Rybalkina
Date:       14-Apr-2023
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  Aurora1825 & crazyrussian123456
Repo:       https://github.com/uofu-cs3500-spring23/assignment8agario-dominators_of_worlds
Date:       14-Apr-2023 Time (of when submission is ready to be evaluated)
Solution:   Agario
Copyright:  CS 3500 and Aurora Zuo & Sasha Rybalkina - This work may not be copied for use in Academic Coursework.
```

# Comments to Evaluators:

This Logger project contains two classes--the custom log provider and the custom logger.
The Custom file logger provider class implements ILoggerProvider and is responsible for
instantiation, configuration and shutdown/cleanup (via IDisposable ) of one or more logger
implementations. The Custom file logger class implements ILogger and contains all the basic
functionalities of a logger.

# Assignments Specific Topics:

Logging, ILogger, IDisposable, Log Level, multithreading.

# References:

    1. Logging: https://learn.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line
    2. ILogger Interface: https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger?view=dotnet-plat-ext-7.0
    3. ForStudents: Lab10 FileLogger
    4. Our own code from A7

# Time Expenditures:

    Total Predicted Hours:      ~20 mins         Total Actual Hours:        ~20 mins

    All the work were done through pair programming, thus the time expenditures for each partner
    are the same.
    
    We used the same Logger codes from our A7, so didn't spend much time work on it because 
    the class already works.


# Partnerships:

All the work were done through pair programming (side by side coding).

# Testing:

We tested the FileLogger programm by opening the server and client, test their functionalities, 
and checks if our log records information/bug/error correctly.

# Branching:

We did all work for this project in the main branch.
