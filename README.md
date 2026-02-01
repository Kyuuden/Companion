# Final Fantasy Rando Companion
[![(latest) release | GitHub](https://img.shields.io/github/release/Kyuuden/Companion.svg?logo=github&logoColor=333333&style=popout)](https://github.com/Kyuuden/Companion/releases/latest)

An auto-tracker for [Free Enterprise, a Final Fantasy IV randomizer](https://ff4fe.com/), [Mystic Quest Randomizer](https://www.ffmqrando.net/) and [Worlds Collide](https://ff6worldscollide.com/).

## Installation
This tool requires [the latest BizHawk version](https://github.com/TASEmulators/BizHawk/releases/latest).
Download the full version from the [latest release](https://github.com/Kyuuden/Companion/releases/latest) that looks like this `FF.Rando.Companion.x.x.x.zip`
Right click on it and select `Extract all...` then navigate to your BizHawkfolder and press `Extract`.
File structure should look like this:
```
BizHawk
└───ExternalTools
│   │   FF.Rando.Companion.dll
│   │
│   └───FF.Rando.Companion
│   │     └───Updater
```

## Usage

Please note: If you are running a pre-5.0 Free Enterprise seed, the SNES core needs to be set to BSNES or BSNESv115+ to enable timing. 
This can be done through ```Config > Preffered Cores > SNES```.

After launching Bizhawk, open the tracker through ```Tools > External Tool > Final Fantasy Rando Companion```
The display will remain grey until a ROM is loaded. 

## Updating
To check if a new version is available, open the about dialog. If there is a new release there will be a 'Update To Latest' button. Clicking it shuts down BizHawk and updates the tool. If it displays "Installation failed" please run the updater manually by going to ```BizHawk\ExternalTools\FF.Rando.Companion\Updater\SimpleLatestReleaseUpdater.exe``` or get the [latest release](https://github.com/Kyuuden/Companion/releases/latest) from GitHub and update manually. If you get an error notifying you that your system lacks the necessary .NET version to run the updater click [the link](https://dotnet.microsoft.com/download/dotnet/5.0/runtime?utm_source=getdotnetcore&utm_medium=referral) and download the x64 and x86 redistributable packages for desktop apps.

## Roadmap

Upcoming features (not in order):
- Livesplit integration
- Completely customizable layouts (drag and drop, reszing etc)
- Save different layouts based on game flagset

## Contributors
* [SchalaKitty](https://www.twitch.tv/schalakitty) - Free Enterprise Boss and Key Item icons.

## Referenced packages and code
* [SimpleLatestReleaseUpdater](https://github.com/TalicZealot/SimpleLatestReleaseUpdater) - self explanitory.
* [SotRandoTools](https://github.com/TalicZealot/SotnRandoTools) - Used as examples for how to do auto updates, better about screen, and more detailed readme.
* [KGy SOFT Drawing Libraries](https://github.com/koszeggy/KGySoft.Drawing) - Easier to use, and better performant bitmap manipulation.
* [Peroquenariz](https://github.com/peroquenariz/FF6WCTools) - Worlds Collide tracking data.
