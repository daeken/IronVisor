Usage
=====

Prior to using the library, you need to resign the main dotnet binary to have the hypervisor entitlement. From the directory into which you checked out the repo (or any containing the plist from the repo):

```
sudo codesign -f -s - --entitlements dotnet-entitlements.plist `which dotnet`
```

Then you can do a Nuget installation of the package.
