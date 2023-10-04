# PineApp - A Flashcard Web Application

# Team members
- [Justas Balčiuūnas](https://github.com/adaspera)
- [Justas Buzys](https://github.com/Buztas)
- [Vitas Eliseenko](https://github.com/suetology)
- [Patricija Katinaite](https://github.com/Patriciakat)
- [Matas Kvainauskas](https://github.com/MatasKvn)

## Description
PineApp is a Single Page Application (SPA) designed to help users study effectively using flashcards. Flashcards have been proven to be an efficient method for memorization, leveraging active recall and spaced repetition. With PineApp, users can create decks of cards on various subjects digitally, ensuring a customizable and efficient study experience. 

## Prerequisites
- .NET SDK
- Node.js & npm (for SPA services)

## Setup & Installation
1. Ensure you have the required software installed (e.g., .NET Core SDK, Node.js).
2. Clone the repository: git clone [repository_url].
3. Navigate to the project directory.
4. Update the `AZURE_SQL_CONNECTIONSTRING` in the configuration with the correct Azure SQL connection string.
5. Run the application: dotnet run.

## Usage
Visit the hosted URL to access PineApp. Create decks, add flashcards, and start your efficient study session.

## Troubleshooting
- **Database Connection Issues**: Ensure the Azure SQL connection string is correctly placed in the application's configuration.
- **Frontend Issues**: If the SPA doesn't load or encounters issues, ensure Node.js dependencies are correctly installed with `npm install`. Check the browser's console for specific errors.
- **API Issues**: If the API isn't responding as expected, check the server logs for potential errors or exceptions.

## Contributing
Pull requests are welcome. Please ensure that any changes are documented and tested before submitting.

## Acknowledgments
- ASP.NET Core for backend support.
- SPA Framework Name (e.g., React, Angular, or Vue) for providing a seamless user interface experience.
- Entity Framework Core for ORM support.
- Azure SQL for database services.
