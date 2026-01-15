Create Proc Get_DeviceFor
(
	@MachineNo Varchar(20)
)
As
Begin
	Select DeviceFor from(
	SELECT MachineNo,'Staff' DeviceFor FROM dbo.PunchMachineConfiguration 
	Union
	SELECT MachineNo,'Student' DeviceFor FROM dbo.PunchMachineConfigurationStudent 
	)T1 Where MachineNo=@MachineNo
End