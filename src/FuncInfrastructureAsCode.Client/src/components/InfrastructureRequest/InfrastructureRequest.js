import React from "react";
import { useParams } from "react-router";
import { Button } from "react-bootstrap";
import ResourceGroups from "../ResourceGroups/ResourceGroups";
import "./InfrastructureRequest.css";
import NetworkInterfaces from "../NetworkInterfaces/NetworkInterfaces";
import Subnets from "../Subnets/Subnets";
import VirtualNetwork from "../VirtualNetwork/VirtualNetwork";
import VirtualMachines from "../VirtualMachines/VirtualMachines";
import { useMsal, useAccount } from "@azure/msal-react";

const InfrastructureRequest = () => {
  const { rowkey } = useParams();

  const { instance, accounts } = useMsal();
  const account = useAccount(accounts[0] || {});

  const setStatusOnActive = () => {
    var baseUrl = process.env.REACT_APP_FUNCTION_API;

    if (account) {
      instance
        .acquireTokenSilent({
          scopes: ["api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user"],
          account: account,
        })
        .then((token) => {
          try {
            fetch(baseUrl + "/InfrastructureRequests/" + rowkey, {
              method: "PUT",
              headers: {
                Accept: "application/json",
                Authorization: "Bearer " + token.accessToken,
              },
              body: JSON.stringify("Active"),
            }).then((response) => {
              console.log("Success:", response);
              window.location.reload();
              return response.json();
            });
          } catch (error) {
            console.error("Error:", error);
          }
        });
    }
  };

  return (
    <div className="InfrastructureRequest">
      <h1>InfrastructureRequest</h1>
      <p>Rowkey: {rowkey}</p>

      <Button
        style={{ marginBottom: "5px" }}
        variant="primary"
        onClick={setStatusOnActive}
      >
        SetStatusActive
      </Button>

      <ResourceGroups requestId={rowkey}></ResourceGroups>
      <NetworkInterfaces requestId={rowkey}></NetworkInterfaces>
      <Subnets requestId={rowkey}></Subnets>
      <VirtualNetwork requestId={rowkey}></VirtualNetwork>
      <VirtualMachines requestId={rowkey}></VirtualMachines>
    </div>
  );
};

InfrastructureRequest.propTypes = {};

InfrastructureRequest.defaultProps = {};

export default InfrastructureRequest;
