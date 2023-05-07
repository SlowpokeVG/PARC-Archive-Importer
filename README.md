# PARC Archive Importer
A tool for injecting files into .PAR archives (used in all PS3 Yakuza games and Binary Domain).
This tool doesn't let you create a new .PAR archive and it doesn't support files bigger than 2GB. 
For those purposes you'll want to use other tools:
- [gibbed/Gibbed.Yakuza0](https://github.com/gibbed/Gibbed.Yakuza0)
- [Kaplas80/ParManager](https://github.com/Kaplas80/ParManager)

However, what this tool offers is injecting files into archives without editing other files, just their placement, so file order is preserved.
Now with compression options!

## How to use
- First you need to unpack your .PAR archive:
  - Use [quickbms](http://aluigi.altervista.org/quickbms.htm) with [this BMS script](http://aluigi.altervista.org/bms/parc.bms)
  - Or download [ParTool](https://github.com/Kaplas80/ParManager/releases) and drag and drop your .PAR onto it
- Edit your files as you like, then start PARC Archive Importer.
- Press **Open Archive** and select your .PAR file to load it. You'll see all the relevant data about the files in the .PAR. If a file is compressed, you'll see what SLLZ version was used.
  - Press *Decimal* or *Hex* to display byte values accordingly. 
- **Revert Changes** will undo every change you've made to the loaded archive. If you accidentally saved over the original .PAR, for example, and you didn't want to do that,
  you can hit this button to get everything back (so long as you haven't closed the program, of course). Then you could save the file again to preserve a copy of the original.
- **Widen** will pad the end of every file in the archive to a multiple of 2048 (0x800 if you like). Sometimes this could be required by the game, YMMV.
- **Open Files** will let you select files to inject into the .PAR. You can do this multiple times if you like, from different folders and so forth.
- **Clear Imports** will clear the list of all of the files you've imported.
- **Inject** will inject every imported file, so long as it has a matching file in the .PAR already. There are three modes for injection:
  - *All Uncompressed* will inject the files as-is.
  - *All Compressed* will inject the files compressed with the SLLZ version you've chosen.
  - *Mirror PAR File* will inject each file compressed or uncompressed based on how it was in the original .PAR archive.
    - The *Auto* option will additionally match the SLLZ version on a per-file basis.
- **Save PAR** lets you save the file, as you'd expect.
  - *Pad Last File* (checked by default) will pad the last file of the saved .PAR to a multiple of 2048. Even with the file widened, this option needs to be checked if you want this behaviour.
- **Mute Warnings** (unchecked by default) means the program won't ask you any questions. I'd recommend leaving this off for a bit until you know what everything does, but you can always hit *Revert Changes* and start over if you haven't closed the program.


If you can, it's always a good idea to keep a copy of the original unaltered .PAR somewhere.