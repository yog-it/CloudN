# Large Az File Upload
An Oqtane module extension for uploading large files to Azure Storage.

This extension uses Access Keys to upload large files to an Azure Storage account.  

## Install
1. Download the latest package version
1. In Oqtane open the Modules Management page from the Admin Dashboard
1. Select Install Module
1. Upload the nuget package file
1. Restart the application from the System Info page 
1. Place the module on a page NOTE: be sure this is a private page or anyone will be able to upload
1. Go into the module's settings
1. On the permissions tab, give edit permission to roles you would like to be able to upload files
1. On the LargeAzFileUpload tab enter the Azure storage account name
1. Enter the access key from your Azure storage 
1. In the default container field select the container you would like the user's drop down list to default to
1. Use the "Users can change containers" checkbox to allow users to see the drop down of container names