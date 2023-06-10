# Changelog

## [Delay Compression] - 2023-05-07

### Added
- An Auto mode for compression version. Only works with Mirror PAR File.
- A progress bar to show how compression / injection is going.

### Changed
- Compression is now delayed until injection. This solves several problems,
  eliminates tricky edge cases that would be confusing for a user anyway,
  and makes the code cleaner as well.
- The Compressed Size column of the import list will no longer display
  any size until a compressed version of that file has been injected,
  since that's the one time when compression takes place. I don't think
  this is a problem in the majority of use cases, and if a file is injected
  compressed and then reverted, the compressed file will still exist in memory,
  meaning the size will still be displayed.
- SLLZ Version options are now enabled and disabled at appropriate times.
- Auto + Mirror PAR File is now the default option, rather than Uncompressed.


## ["Knock It Outta The PARC" Edition] - 2023-05-05

### Added
- Imported some of ParLibrary for SLLZ compression.
  Removed its dependencies on Yarhl and Ionic.
- A dropdown to pick compression option for injected files.
  Currently available: Uncompressed (default), Compressed, Mirror PAR File.
- A choice between SLLZ v1 and v2 for compression.
- A button to revert all unsaved changes to the PAR file.
- A button to clear the import file list, which also reverts injections.
- An option to display file sizes and offsets in hex.
- New columns in the import pane: Injected, Comp. Size.
  Some reordering and resizing of columns to mirror the other pane.
- A few warning message boxes about program behaviour.
- An option to mute those warning messages.
- An option to pad the last file in the saved PAR to 2048.
  Some files seem to do this, some don't. It probably doesn't matter.
- A charming context menu.

### Changed
- Used .NET Upgrade Assistant to move the codebase to .NET 7.0.
  ParLibrary's Compressor.cs uses ReadOnlySpan<T>, and I wanted to
  accomodate that rather than changing it. I haven't run into any issues
  as a result of the change. Everything seems good.
- Files can now be injected compressed if desired. This is a helpful option
  for those occasions when games are finicky about uncompressed files,
  but when it would also be inappropriate to batch-compress all files.
  There are some cases in Kenzan where this is necessary, as an example.
- Files are compressed before injection (currently only by changing the
  Compression Option setting via the dropdown). This is done in order
  to correctly report the compressed size prior to injection. Care is
  taken to make sure the compressed files are appropriately handled
  and stored as circumstances require.
- Widening files is no longer a requirement. I've had no problems manually
  copying files into unwidened PARs for Kenzan, so I know it's not
  a requirement for at least that game. Widening is a feature when needed.
  (The default behaviour is now to pad to 2048-byte chunks only when
  the next file in the archive would cross that barrier, which was
  the behaviour of the RGG in-house tool in the PS3 days at least.)
- Importing new files no longer wipes the old import list by default.
  The old behaviour is still present with the Clear List option,
  or via the warning prompt that comes up when you load more files.
- The byte array, InterOpenedArchive, is now only accessed as part of the
  saving process, and never edited after its creation. File injections
  change the column values of listArch directly, and nothing else.
  When a save is initiated, a copy is made of the archive, and its
  file headers in the copy are updated to reflect the values in listArch,
  which have already been calculated by that time. This also means
  that ParseSourceArray is called only when loading a PAR file.
- Many helper functions were simplified or eliminated by the above change,
  and repetition of work has been cut down as a result.
- Changed the 2048 / multiplier calculations to modulo, for readability.
  It shouldn't have any noticeable impact on performance at this scale.
- Controls now resize with the window.

### Fixed
- If files had been injected, and then that list of files was cleared away
  (for example, with the old file-loading semantic where new file imports
  would clear and replace the old list) the PAR file would still have its
  headers updated to reflect the old injections. Saving the file would
  result in a corrupted file. This was fixed when the import semantics
  were changed.

### Potential for New Bugs
- It works with Kenzan. That's the full extent of my testing.
  I haven't tested SLLZ v2 basically at all.