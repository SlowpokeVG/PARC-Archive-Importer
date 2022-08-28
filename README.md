# PARC Archive Importer
A tool for injecting files into .PAR archives (used in all PS3 Yakuza games and Binary Domain).
This tool doesn't let you create a new .PAR archive and it doesn't support files bigger than 2GB. 
For those purposes it is recommended to use more modern tools:
- [gibbed/Gibbed.Yakuza0](https://github.com/gibbed/Gibbed.Yakuza0)
- [Kaplas80/ParManager](https://github.com/Kaplas80/ParManager)

However, what this tool offers is injecting files into archives without editing other files, just their placement, so file order is preserved.

## How to use
- First you need to use [quickbms](http://aluigi.altervista.org/quickbms.htm) tool with [BMS script](http://aluigi.altervista.org/bms/parc.bms) to unpack your archive;
- Then edit your files and run this program;
- Press "Open Archive" and select your .Par file;

**This step is _extremely_ important**
- You have to convert this archive to be able to replace files in it, so press "Widen" and wait until it's complete;
  - This will ensure that all files are separated in the archive and each takes 2048x bytes of space;
  - After it's done you can save this archive, so you won't have to "Widen" it again.

**If you won't do this, game will have problems reading files properly.**

- Add files you want to replace via "Open Files" button;
- Press "Inject";
  - This will change the header of your archive in the memory, so if you change your mind or want to inject some other files - restart the program;
- Save archive after using "Inject".
