# B-Launcher

The official cross-platform launcher for **B-World**, built with Avalonia UI and .NET!

![B-Launcher UI](screenshot.png)

## About

_B-Launcher is the dedicated launcher for the procedural open-world game B-World. It simplifies downloading and launching the game on both Linux and Windows platforms. The launcher is built using the MVVM architecture for clear separation of UI and logic, making it easy to extend and maintain._

## Features

- **Cross-platform support** for Linux and Windows
- Easy and fast game download and launch
- Built with Avalonia UI for a modern and responsive interface
- Lightweight and efficient .NET-based MVVM architecture
- Initial version 0.1 with core launcher functionality

## Latest Release: Version 0.3
_Release Date:_ november 2025

### What’s New in 0.3

- **Multi-packet downloading** with automatic retry
- UI Improvements
- Better support for Wayland-compositors and Windows

### Roadmap

- Implement automatic updates for launcher and game
- Optimize performance and responsiveness
- Improve UI/UX with custom theming

## For Developers

If you want to build your own launcher based on B-Launcher:

- The project uses **.NET** and can be built using the `dotnet` CLI:
```sh
dotnet build
dotnet publish --release <your platform>
```

- The code follows the **MVVM (Model-View-ViewModel)** pattern to separate UI from logic, making it clean and maintainable.
- Feel free to fork and customize the launcher for your own projects!

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any suggestions or improvements.

---

## License

*This project is licensed under the MIT License.*

Thank you for supporting B-World!  
*by Vaccuum*
