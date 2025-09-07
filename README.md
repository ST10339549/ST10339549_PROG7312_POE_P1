# Municipal Services – Part 1 (Report Issues)

Clean .NET 9 Windows Forms app implementing:
- Startup **Main Menu** (only *Report Issues* enabled)
- **Report Issues** form with Location, Category, Description, Attachment
- **Engagement** feedback: status, progress bar, tracking number
- **Clean structure**: Domain, Validation, Application (services), Infrastructure (repo), Presentation (UI)

## Build & Run
1. Open folder in Visual Studio 2022 (17.10+) with .NET 9 SDK installed.
2. Build → Build Solution.
3. Run (F5). Main menu will appear.

## Notes
- Data is stored in-memory (`InMemoryIssueRepository`) for Part 1 (per rubric).
- Parts 2 & 3 can reuse the same service/repository.