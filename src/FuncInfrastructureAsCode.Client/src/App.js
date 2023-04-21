import "./App.css";
import React, { useEffect } from "react";
import { useMsal, useAccount } from "@azure/msal-react";
import AppNavbar from "./components/AppNavbar/AppNavbar";
import Home from "./components/Home/Home";
import ResourceGroups from "./components/ResourceGroups/ResourceGroups";
import NetworkInterfaces from "./components/NetworkInterfaces/NetworkInterfaces";
import Container from "react-bootstrap/Container";
import { BrowserRouter, Switch, Route } from "react-router-dom";

function App() {
  const { instance, accounts, inProgress } = useMsal();
  const account = useAccount(accounts[0] || {});

  useEffect(() => {
    // console.log(account);
    if (account) {
      instance
        .acquireTokenSilent({
          scopes: ["api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user"],
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
              return response.json();
            });
        });
    }
  }, [account, instance]);

  if (accounts.length > 0) {
    return (
      <div>
        <AppNavbar />
        <Container style={{marginTop: 15 + "px"}}>
          <BrowserRouter>
            <Switch>
              <Route exact path="/">
                <Home />
              </Route>
              <Route path="/resourceGroups">
                <ResourceGroups />
              </Route>
              <Route path="/networkInterfaces">
                <NetworkInterfaces />
              </Route>
            </Switch>
          </BrowserRouter>
        </Container>
      </div>
    );
  } else if (inProgress === "login") {
    return <span>Login is currently in progress!</span>;
  } else {
    return <span>There are currently no users signed in!</span>;
  }
}

export default App;
