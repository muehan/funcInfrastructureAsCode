import "./App.css";
import React, { useEffect } from "react";
import { useMsal, useAccount } from "@azure/msal-react";

function App() {
  const { instance, accounts, inProgress } = useMsal();
  const account = useAccount(accounts[0] || {});
  // const [apiData, setApiData] = useState(null);

  useEffect(() => {
    console.log(account);
    if (account) {
      instance
        .acquireTokenSilent({
          scopes: ["User.Read"],
          account: account,
        })
        .then((response) => {
          console.log(response);
          if (response) {
            console.log(response);
            // callMsGraph(response.accessToken).then((result) =>
            //   setApiData(result)
            // );

            fetch(
              "https://funcinfrastructureascode.azurewebsites.net/api/Authtest",
              {
                headers: {
                  Accept: 'application/json',
                  Authentication: "Bearer " + response.accessToken,
                  'X-Custom-Header': 'header value'
                },
              }
            )
              .then((response) => {
                console.log(response);
                return response.json();
              })
              .then((data) =>
                this.setState({ totalReactPackages: data.total })
              );
          }
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
