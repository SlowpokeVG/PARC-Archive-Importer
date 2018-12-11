# PARC Archive Importer
A tool for injecting files into *PAR archives (used in all PS3 Yakuza games and Binary Domain).

## How to use
- First you need to use [quickbms](http://aluigi.altervista.org/quickbms.htm) tool with [BMS script](http://aluigi.altervista.org/bms/parc.bms) to unpack your archive;
- Then edit your files and run this program;
- Press "Open Archive" and select your .Par file;

**This step is _extremely_ important**
- You have to convert this archive to be able to replace files in it, so press "Widen" and wait until it's complete;
  - This will ensure that all files are separated in the archive and each takes 2048x bytes of space;
  - After it's done you can save this archive, so you won't have to "Widen" it again.
If you won't do this, game will have problems reading files properly.

- Add files you want to replace via "Open Files" button;
- Press "Inject";
  - This will change the header of your archive in the memory, so if you change your mind or want to inject some other files - restart the program;
- Save archive after using "Inject".
