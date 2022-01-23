# CustomerRegAPI
Small API to crud customer details to a DB

Welcome to my small CustomerRegAPI.

This solution has been built with registration data being written to MS SQL Server. The schema can be found with in the Database folder at the top level of the solution.
Both a Database and a table called CustomerDetails will be required to run the application.

As specified additional authentication has not been added.

The API has swagger attached, for ease of testing, I have made sure that the Post endpoint works as expected.
The other endpoints were added during the scoffolding process as standard, so I have left them in there for testing purposes, but can be removed if strictly one end point was required.

Validation has been added using a seperate business logic project, to display the use of dependancy injection. If I had more time, I would also break the entity framework logic to a seperate project, also with DI,
to ensure complete scalability at a later date with other projects being able to plug into the same db with ease and keep each of the logic sets doing just one job.

Email validation is a bit of a minefield, so I have used someone elses regex to perform it, and placed a reference there to make it known where the work came from.
