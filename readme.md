
#create user for deployment
az ad sp create-for-rbac --name "deploymentuser" --role contributor --scopes /subscriptions/<<subscriptionId>>/resourceGroups/<<resourcegroupid>> --sdk-auth