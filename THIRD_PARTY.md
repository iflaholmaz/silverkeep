# Third-party sources

- **Electron 44.7.0:** desktop runtime. Official distribution: https://github.com/electron/electron/releases/tag/v44.7.0 . The packaged runtime includes `LICENSE` and `LICENSES.chromium.html`; those files are retained in distributed builds.
- **Albion Online Data Project:** public regional prices and history. https://www.albion-online-data.com/api/ . Requests are batched and rate-limited; timestamps and offline cache status are shown.
- **ao-data / ao-bin-dumps:** item identifiers, names, production and upgrade requirements. https://github.com/ao-data/ao-bin-dumps . Build-time metadata is generated locally; raw game dumps are not committed to this repository.
- **Albion Online / Sandbox Interactive:** game item names, game data and images. Item images are requested from `https://render.albiononline.com/v1/item/`. All game-related rights remain with the relevant owners.

The application uses system Segoe UI/Tahoma fonts; no commercial font files are bundled in this source package. Application code, installer helper, logo and repository banner are project work signed **xvv**. Third-party components are not claimed as original project work.
