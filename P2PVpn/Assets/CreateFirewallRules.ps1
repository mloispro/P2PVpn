#Get-NetFirewallRule -DisplayName "*torrent*" | Get-NetFirewallPortFilter 

#Get-NetFirewallRule -DisplayName *torrent* -TracePolicyStore

Remove-NetFirewallRule -DisplayName "torrentP2PMon UDP"
Remove-NetFirewallRule -DisplayName "torrentP2PMon TCP"
Remove-NetFirewallRule -DisplayName "qBittorrentP2PMon"

New-NetFirewallRule -DisplayName "torrentP2PMon UDP" -Action Block -Direction Inbound -DynamicTarget Any -EdgeTraversalPolicy Block -Enabled True -IcmpType Any -InterfaceType Any -LocalOnlyMapping $False -LocalPort 6881-6889 -LooseSourceMapping $False -Profile Any -Protocol UDP -RemotePort Any

New-NetFirewallRule -DisplayName "torrentP2PMon TCP" -Action Block -Direction Inbound -DynamicTarget Any -EdgeTraversalPolicy Block -Enabled True -IcmpType Any -InterfaceType Any -LocalOnlyMapping $False -LocalPort 6881-6889 -LooseSourceMapping $False -Profile Any -Protocol TCP -RemotePort Any

New-NetFirewallRule -DisplayName "qBittorrentP2PMon" -Action Block -Direction Inbound -DynamicTarget Any -EdgeTraversalPolicy Block -Enabled True -IcmpType Any -InterfaceType Any -LocalOnlyMapping $False -LocalPort Any -LooseSourceMapping $False -Profile Domain,Private -Program "C:\Program Files (x86)\qBittorrent\qbittorrent.exe" -Protocol Any -RemotePort Any

New-NetFirewallRule -DisplayName "qBittorrentP2PMon" -Action Block -Direction Outbound -DynamicTarget Any -EdgeTraversalPolicy Block -Enabled True -IcmpType Any -InterfaceType Any -LocalOnlyMapping $False -LocalPort Any -LooseSourceMapping $False -Profile Domain,Private -Program "C:\Program Files (x86)\qBittorrent\qbittorrent.exe" -Protocol Any -RemotePort Any
