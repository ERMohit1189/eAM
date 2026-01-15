Alter Proc Get_ALLRegisteredDevice
As
Begin
	SELECT IpAddress,PortNo,Password,IIF(IsPush=1,'Yes','No') IsPush,MachineNo FROM dbo.PunchMachineConfiguration 
	Union
	SELECT IpAddress,PortNo,Password,IIF(IsPush=1,'Yes','No') IsPush,MachineNo FROM dbo.PunchMachineConfigurationStudent 
End