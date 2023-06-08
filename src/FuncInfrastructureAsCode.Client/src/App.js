import "./App.css";
import React, { useEffect } from "react";
import { useMsal, useAccount } from "@azure/msal-react";
import AppNavbar from "./components/AppNavbar/AppNavbar";
import Home from "./components/Home/Home";
import ResourceGroups from "./components/ResourceGroups/ResourceGroups";
import NetworkInterfaces from "./components/NetworkInterfaces/NetworkInterfaces";
import Subnets from "./components/Subnets/Subnets";
import Container from "react-bootstrap/Container";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import VirtualNetwork from "./components/VirtualNetwork/VirtualNetwork";
import VirtualMachines from "./components/VirtualMachines/VirtualMachines";
import RequestVirtualMachine from "./components/RequestVirtualMachine/RequestVirtualMachine";

function App() {
  const { instance, accounts, inProgress } = useMsal();
  const account = useAccount(accounts[0] || {});

  useEffect(() => {
    if (account) {
      instance.acquireTokenSilent({
        scopes: ["api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user"],
        account: account,
      });
    }
  }, [account, instance]);

  if (accounts.length > 0) {
    return (
      <div>
        <BrowserRouter>
          <AppNavbar />
          <Container style={{ marginTop: 15 + "px" }}>
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
              <Route path="/subnets">
                <Subnets />
              </Route>
              <Route path="/virtualNetowrk">
                <VirtualNetwork />
              </Route>
              <Route path="/virtualMachines">
                <VirtualMachines />
              </Route>
              <Route path="/request/virtualMachines">
                <RequestVirtualMachine />
              </Route>
            </Switch>
          </Container>
        </BrowserRouter>
      </div>
    );
  } else if (inProgress === "login") {
    return <span>Login is currently in progress!</span>;
  } else {
    return <span>There are currently no users signed in!</span>;
  }
}

export default App;
