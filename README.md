# 📦 Sims 2 Package Manager

S2PK, or Sims 2 Package Manager, aims to provide a simple cross-platform solution to package management.

## 💡 Why This Exists

S2PK was created to address the need for a reliable and efficient cross-platform package management system for The Sims 2 on Linux and macOS. Players on these platforms often face challenges in managing mods due to the lack of a unified solution that their Windows counterparts solved a long time ago.

## 🛣️ Project Roadmap

| Phase | Goal                                        | Status |
| ----- | ------------------------------------------- | ------ |
| v0.1  | Core package manager                        | ✅      |
| v0.2  | Config file with default destination        | 🔜     |
| v1.0  | Stable "Release" version with documentation | 🔜     |

## 🧩 Tech Stack

- .NET 8.0
- C# (focused on clarity, safety, minimalism)
- System.CommandLine for CLI parsing (no external libraries)
- Pure backend logic (no UI planned)

## Example

```shell
s2pkg pack -s ./mods -o output.s2pk
```

```
s2pkg unpack -p ./output.s2pk -d "%USERPROFILE%\Documents\EA Games\The Sims 2\Downloads"
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
