# Silverkeep 4.0 · xvv

Run **Silverkeep-Kurulum.exe** to install or update the Windows desktop app. Existing local records are preserved.

Choose **English** in the language selector at the top right. The selection is stored locally and survives restart. Item names, menus, calculators, dialogs, numbers, dates and the tray menu follow the selected language. Your custom character, storage, session and route names are preserved.

- **Market:** choose an item in the left list, compare city prices and open Chart for history.
- **Production:** crafting, refining, route comparisons, focus, laborers and production plans.
- **Storage:** containers, transfers, CSV and equipment carts.
- **Gathering:** sessions, resource reports and manual Avalon connections.
- **Island:** crop/animal cycles and Windows reminders.
- **Calculators:** loot purchase percentages, party splits, transport and actual income/expenses.

F1 opens Market, F2 Craft, F3 Storage and F4 Gathering. Ctrl+K searches the item catalog. Closing the window keeps the app in the tray; use the tray menu to quit.

Market prices come from the Albion Online Data Project. Missing or stale prices are marked. Personal storage, orders, routes and session data are entered locally; the application does not automatically read your game account. Windows reminders require the application to be running.

For compatibility, local data remains in `%APPDATA%\AlbionDesk` and the installation remains in `%LOCALAPPDATA%\Programs\AlbionDesk`. Backups and JSON import/export are available in Settings.

## Connected workflows in 4.0

The app now opens directly to Black Market. Choose the item, batches and material city, then calculate profit. Returns, focus and fees are under the expandable options. Stale material or sale prices are marked.

Bulk loot is under Calculators; stock sales and reversible action history are under Storage. Corrections retain the original action, and dependency checks prevent overwriting newer movements. Actions before 4.0 have no undo snapshot. Templates and preflight checks are under Production; held-stock age is under Storage; goals are under Calculators. Quick calculator opens a separate always-on-top-capable window. Actual sale revenue and book costs are linked in the ledger: do not enter the same costs again manually.
