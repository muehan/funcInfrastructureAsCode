import "./RequestVirtualMachine.css";
import React from "react";
import Form from "react-bootstrap/Form";
import Container from "react-bootstrap/Container";
import Button from "react-bootstrap/Button";
import FormResourceGroup from "../FormResourceGroup/FormResourceGroup";
import FormVirtualNetwork from "../FormVirtualNetwork/FormVirtualNetwork";
import FormSubnet from "../FormSubnet/FormSubnet";
import FormNetworkInterface from "../FormNetworkInterface/FormNetworkInterface";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";
import FormVirtualMachine from "../FormVirtualMachine/FormVirtualMachine";
import { useMsal, useAccount } from "@azure/msal-react";

const RequestVirtualMachine = () => {
  const { instance, accounts } = useMsal();
  const account = useAccount(accounts[0] || {});

  const [response, setResponse] = React.useState({});
  const [error, setError] = React.useState({});

  var baseUrl = process.env.REACT_APP_FUNCTION_API;

  const {
    resourceGroup,
    virtualNetwork,
    subnet,
    networkInterface,
    virtualMachine,
  } = useVirtualMachine();

  const handleSubmit = (event) => {
    var data = {
      resourceGroup: resourceGroup,
      virtualNetwork: virtualNetwork,
      subnet: subnet,
      networkInterface: networkInterface,
      virtualMachine: virtualMachine,
    };

    const postData = async () => {
      if (account) {
        instance
          .acquireTokenSilent({
            scopes: [
              "api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user",
            ],
            account: account,
          })
          .then((token) => {
            try {
              fetch(baseUrl + "/AddVirtualMachine", {
                method: "POST",
                headers: {
                  Accept: "application/json",
                  Authorization: "Bearer " + token.accessToken,
                },
                body: JSON.stringify(data),
              })
                .then((response) => {
                  return response.json();
                })
                .then((data) => {
                  setResponse(data);
                });
            } catch (error) {
              setError(error);
            }
          });
      }
    };

    postData();

    if(error) {
      alert(error);
    }
    else {
      alert(response);
    }

    event.preventDefault();
  };

  return (
    <Container fluid>
      <Form onSubmit={handleSubmit}>
        <FormResourceGroup />
        <hr />
        <FormVirtualNetwork />
        <hr />
        <FormSubnet />
        <hr />
        <FormNetworkInterface />
        <hr />
        <FormVirtualMachine />
        <hr />
        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    </Container>
  );
};

RequestVirtualMachine.propTypes = {};

RequestVirtualMachine.defaultProps = {};

export default RequestVirtualMachine;
