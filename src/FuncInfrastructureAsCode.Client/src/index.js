import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";

import { MsalProvider } from "@azure/msal-react";
import { PublicClientApplication } from "@azure/msal-browser";
import { UnauthenticatedTemplate, MsalAuthenticationTemplate  } from "@azure/msal-react";
import { InteractionType } from "@azure/msal-browser";

import 'bootstrap/dist/css/bootstrap.min.css';

const configuration = {
  auth: {
    clientId: "e44368db-bfee-45f1-b34d-1d3932593964",
    redirectUri: window.location.origin,
    authority: "https://login.microsoftonline.com/8d9ee075-c4e3-45ea-b93f-ca7916eba85d"
  },
};

const pca = new PublicClientApplication(configuration);

const root = ReactDOM.createRoot(document.getElementById("root"));

function ErrorComponent({error}) {
  return <p>An Error Occurred: {error.message}</p>;
}

function LoadingComponent() {
  return <p>Authentication in progress...</p>;
}

const authRequest = {
  scopes: ["api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user"]
};

root.render(
  <React.StrictMode>
    <MsalProvider instance={pca}>
       <MsalAuthenticationTemplate 
            interactionType={InteractionType.Redirect} 
            authenticationRequest={authRequest}
            errorComponent={ErrorComponent} 
            loadingComponent={LoadingComponent}
        >
             <App />
        </MsalAuthenticationTemplate>
      <UnauthenticatedTemplate>
        <p>You are not logged in</p>
      </UnauthenticatedTemplate>
    </MsalProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
