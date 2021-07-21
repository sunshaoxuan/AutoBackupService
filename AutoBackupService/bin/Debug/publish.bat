net stop AutoBackupService
del "C:\Program Files\CNDRP.COM\AutoBackupService\*.log"
del "C:\Program Files\CNDRP.COM\AutoBackupService\AutoBackupService.exe"
copy "C:\Users\sunsx\Source\Repos\sunshaoxuan\AutoBackupService\AutoBackupService\bin\Debug\AutoBackupService.exe" "C:\Program Files\CNDRP.COM\AutoBackupService\AutoBackupService.exe"
net start AutoBackupService