# AI Assistant Instructions

Always consult the documentation in `.context/` and `constitution.md` before generating code:
- Strictly adhere to layer boundaries in `.context/01-architecture.md`.
- Never place EF Core logic in `Core`.
- Never bypass the ERD constraints in `.context/02-data-model.md`.
- Explain concepts clearly and concisely whenever introducing new patterns.