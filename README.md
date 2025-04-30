# 📦 Sims 2 Package Manager

S2PK, or Sims 2 Package Manager, aims to provide a simple cross-platform solution to package management.

## ❓ Why This Exists

While The Sims 2 runs well on Linux through Wine, especially with Lutris setups, and natively from macOS, only the Windows community had varies mod managers and installers compared to their cross-platform counterparts. Later installments addressed this dilemma, but TS2 never got that treatment.

`s2pk` aims to fill this gap: a no-nonsense CLI utility for packaging, sharing, and managing mods across all platforms. It’s small, clean, and plays nicely with scripts, backups, and version control. In theory, it would be possible to use it on legacy front ends.

## 🚀 Features

- First-class CLI support for modders on non-Windows platforms
- Custom default unpacking directory for The Sims 2 & 3
- Pack `.packages` into portable `.s2pk` archives

## 🛣️ Project Roadmap

| Phase | Goal                                        | Status |
| ----- | ------------------------------------------- | ------ |
| v0.1  | Core package manager                        | ✅     |
| v0.2  | Config file with default paths              | ✅     |
| v0.3  | Sims 3 support with `s3pk` extention        | 🔜     |
| v0.x  | Target .NET 10                              | 🔜     |
| v1.0  | Stable "Release" version with documentation | 🔜     |

## 🎯 Stretch Goals

- [ ] Manifest validation
- [ ] `s1pk` soft fork

## 🧩 Tech Stack

- .NET 8.0
- C# (focused on clarity, safety, minimalism)
- System.CommandLine for CLI parsing (no external libraries)
- Pure backend logic (no UI planned)

## 📐 Design Principles

- Stay true to UNIX design philosophy
- Simple with no bloat or feature creep
- Portable and clean

## 🛠️ Installation

You can build yourself with `make`, or use the installer script:

```shell
./install.sh ./dist/s2pk-linux/s2pkg
```

Make sure `~/.local/bin` is in your `PATH`:

```shell
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc
```

## 🗜️ Usage

```shell
s2pk pack -s ./mods -o output.s2pk
```

```shell
s2pk unpack -p ./output.s2pk -d "%USERPROFILE%\Documents\EA Games\The Sims 2\Downloads"
```

## 🗓️ Update Cycle

| Type         | Frequency        | Notes                                    |
| ------------ | ---------------- | ---------------------------------------- |
| Minor Update | Every 3–6 months | Small enhancements, non-breaking changes |
| Patch Update | As needed        | Bug fixes, security updates              |
| Major Update | As needed        | Framework upgrades, major refactors      |

- Reserve months: June (Mid-Year Chill) & December (End-Year Freeze)

## 🛡️ Status

- [x] Active Support
- [ ] Limited Support (Security patches only)
- [ ] Maintenance Mode (Dependency-only updates)
- [ ] Archived (No active work planned)

## 🎮 Relaxation Practices

- 20% creative/recovery space built into development
- Mandatory cooldowns after major launches (minimum 1 week)
- Crisis Mode Activates if:
  - Critical vulnerabilities
  - Framework-breaking issues

## 🗒️ License

I license this project under the GPL v3 license - see [LICENSE](LICENSE) for details.
