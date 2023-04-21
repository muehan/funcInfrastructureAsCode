import "./App.css";
import React, { useEffect } from "react";
import { useMsal, useAccount } from "@azure/msal-react";

function App() {
  const { instance, accounts, inProgress } = useMsal();
  const account = useAccount(accounts[0] || {});

  useEffect(() => {
    // console.log(account);
    if (account) {
      instance
        .acquireTokenSilent({
          scopes: [
            "api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user",
          ],
          account: account,
        })
        .then((response) => {

          fetch(
            "https://funcinfrastructureascode.azurewebsites.net/api/Authtest",
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
            .then(response => {
              console.log(response);
            })
        });
    }
  }, [account, instance]);

  if (accounts.length > 0) {
    return (
      <>
        <span>There are currently {accounts.length} users signed in!</span>
      </>
    );
  } else if (inProgress === "login") {
    return <span>Login is currently in progress!</span>;
  } else {
    return <span>There are currently no users signed in!</span>;
  }
}

export default App;
