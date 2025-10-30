> [!IMPORTANT]
> Using this Harmony mod is not supported by Facepunch.
> 
> You may be required to manually delete your player blueprints database if
> Facepunch decides to change the database schema.
>
> Please report any issues related to blueprints or persistence that you
> experience while using this Harmony mod via
> [GitHub Issues](https://github.com/flerouwu/Rust.PreventBlueprintWipes/issues).

# Rust.PreventBlueprintWipes

Rust Harmony mod that prevents player blueprints from wiping every force wipe.

**NOTE:** This mod does not prevent blueprints from wiping on every protocol
version increment (e.g. when setting your server game mode to Hardcore).

## Installation

> [!WARNING]
> This mod does not automatically migrate your existing database file. You are
> required to manually rename your existing `player.blueprints.*.db` (where `*` is a number)
> database file to `player.blueprints.db`.

1. Download `Rust.PreventBlueprintWipes.dll` from the [latest release](https://github.com/flerouwu/Rust.PreventBlueprintWipes/releases/latest),
    and place it into `HarmonyMods/` on your server.
2. Rename your existing blueprints database (see above warning).
3. Restart your server.

## Building from Source

### Requirements

- .NET 10.0 or newer
- [DepotDownloader](https://github.com/SteamRE/DepotDownloader) available on your path as `depotdownloader`.

### Steps

> [!CAUTION]
> As good security practice, you should read the build files that are contained
> in this repository before executing them! Any project on GitHub may contain
> malicious software, so you should always be cautious.

1. Clone from Git:
    ```sh
    git clone https://github.com/flerouwu/Rust.PreventBlueprintWipes
    cd Rust.PreventBlueprintWipes
    ```
2. Download required assembly references:
    ```sh
    # You only need to run the following command if you have previously
    # downloaded the references. It will error if you haven't done so.
    rm -rf .references/

    ./download-references.sh
    ```

3. Build the project.
    ```sh
    dotnet build Rust.PreventBlueprintWipes.sln --configuration Release
    ```
