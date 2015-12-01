# Get the ID and security principal of the current user account
#$myWindowsID=[System.Security.Principal.WindowsIdentity]::GetCurrent()
#if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) { Start-Process powershell.exe "-NoProfile -ExecutionPolicy Bypass -File `"$PSCommandPath`"" -Verb RunAs; exit }

#Enter-PSSession -Credential(Get-Credential) -ComputerName olsonhome


#$credential = Get-Credential -Credential olsonhome\Mitch

#$Session = New-PSSession -ComputerName olsonhome -Credential $credential

#Create credential object
                #$Username = "Mitch"
                #$SecurePassWord = ConvertTo-SecureString -AsPlainText 1 -Force
                #$Cred = New-Object -TypeName "System.Management.Automation.PSCredential" -ArgumentList $Username, $SecurePassWord -Authentication Credssp
            #Create session object with this
                #$Session = New-PSSession -ComputerName olsonlhome -credential $Cred
            #Invoke-Command
#          $Script_Block = "C:\Projects\P2PVpn\P2PVpn\Assets\RestartMediaServer.ps1"           
#$Script = $executioncontext.invokecommand.NewScriptBlock($Script_Block)                       
#$Job = Invoke-Command -Session $Session -Scriptblock $Script -run 
  #Close Session
#                Remove-PSSession -Session $Session

#$creds = Get-Credential  #-Credential olsonhome\Mitch
$service = "PS3 Media Server"
<#
$pms = get-process pms -ComputerName olsonhome -IncludeUserName  olsonhome\Mitch

if (!$pms) 
{ 
    start-process "C:\Program Files (x86)\PS3 Media Server\pms.exe" -ComputerName olsonhome
}
#>
Set-Service $service -ComputerName olsonhome -startupType automatic
Get-Service -Name $service -ComputerName olsonhome | reStart-service