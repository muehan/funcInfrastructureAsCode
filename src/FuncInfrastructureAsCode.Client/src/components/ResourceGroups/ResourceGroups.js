import "./ResourceGroups.css";
import React, { useEffect } from "react";
import { useMsal, useAccount } from "@azure/msal-react";

const ResourceGroups = () => {
  const { instance, accounts } = useMsal();
  const account = useAccount(accounts[0] || {});

  useEffect(() => {
    if (account) {
      instance
        .acquireTokenSilent({
          scopes: ["api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user"],
          account: account,
        })
        .then((response) => {
          fetch(
            "https://funcinfrastructureascode.azurewebsites.net/api/ResourceGroups",
            {
              headers: {
                Accept: "application/json",
                Authorization: "Bearer " + response.accessToken,
              },
            }
          )
            .then((response) => {
              console.log(response);
              return response.json();
            })
            .then((response) => {
              console.log(response);
            });
        });
    }
  }, [account, instance]);

  return <div className="ResourceGroups">ResourceGroups Component</div>;
};

ResourceGroups.propTypes = {};

ResourceGroups.defaultProps = {};

export default ResourceGroups;
