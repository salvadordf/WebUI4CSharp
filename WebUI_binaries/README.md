# WebUI binaries
These are the WebUI binaries required to run the demos.

The demos declare the _WEBUIDEMO_ constant to load them from these directories but other applications should deploy them in the same directory as the executable.

The 64 bits binaries are a copy from the [WebUI Development Build](https://github.com/webui-dev/webui/releases/tag/nightly) package but the 32 bits binaries were built using [ZIG](https://ziglang.org/).

## Building

* Download [ZIG](https://ziglang.org/download/).
* Decompress the package.
* Add the ZIG directory to the PATH environment variable.
* Open a command prompt window
* Change to the WebUI directory.
* Type this command :
```
	zig build -Dtarget=x86-windows-gnu -Ddynamic=true -Denable-tls=false -Doptimize=ReleaseFast
```
* The binaries will be created in the zig-out\lib directory after a few seconds.

