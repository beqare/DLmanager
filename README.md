# DLmanager

A simple and lightweight WPF application for downloading files from a given URL with progress tracking and cancellation capabilities.

---

## Features

* **Download Files:** Easily download files by providing their URL.
* **Progress Tracking:** Monitor the download progress in real-time, including percentage, downloaded size, total size, and download speed.
* **Cancel Downloads:** Stop ongoing downloads at any time with a dedicated cancel button.
* **User-Friendly Interface:** A clean and intuitive dark-themed UI built with WPF.
* **Automatic Download Path:** Downloads are saved directly to your user's `Downloads` folder.

---

## Screenshots

![DEMO](https://raw.githubusercontent.com/beqare/DLmanager/refs/heads/master/github/screenshot.png)

---

## How to Use

1.  **Enter URL:** Paste the download link into the "Download URL" text box.
2.  **Start Download:** Click the "Start Download" button.
3.  **Monitor Progress:** The progress bar and status text will update as the file downloads.
4.  **Cancel Download:** To stop an active download, click the "Cancel Download" button.

---

## Technologies Used

* **WPF (.NET 8.0):** For the graphical user interface.
* **C#:** The programming language used for the application logic.
* **`System.Net.Http`:** For handling HTTP requests and file downloads.
* **`System.Threading.Tasks` & `CancellationTokenSource`:** For asynchronous operations and download cancellation.

---

## Installation (for Users)

Currently, there is no installer. You can download the latest release from the [Releases](https://github.com/beqare/DLmanager/releases) section and run the executable.

---

## Building from Source (for Developers)

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/beqare/DLmanager.git
    ```
2.  **Open in Visual Studio:** Open the `DLmanager.sln` file in Visual Studio 2022 (or newer).
3.  **Restore NuGet Packages:** Build the solution to automatically restore any required NuGet packages.
4.  **Run:** Press `F5` to run the application in debug mode, or `Ctrl+Shift+B` to build it.
5.  **Publish:** To create a distributable version, right-click on the project in Solution Explorer, select `Publish`, and configure your desired settings (e.g., `win-x64` as target runtime for framework-dependent single file).

---

## Contributing

Feel free to fork the repository, open issues, or submit pull requests. All contributions are welcome!

---

## License

This project is licensed under the [MIT License](LICENSE).