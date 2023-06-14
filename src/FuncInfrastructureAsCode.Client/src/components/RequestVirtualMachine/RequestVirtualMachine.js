import "./RequestVirtualMachine.css";
import React from "react";
import Toast from "react-bootstrap/Toast";
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
import FormRequester from "../FormRequester/FormRequester";

const RequestVirtualMachine = () => {
  const { instance, accounts } = useMsal();
  const account = useAccount(accounts[0] || {});

  // const [response, setResponse] = React.useState({});
  const [error, setError] = React.useState({});
  const [showError, setShowError] = React.useState(false);

  var baseUrl = process.env.REACT_APP_FUNCTION_API;

  const {
    infrastructureRequest,
    resourceGroup,
    virtualNetwork,
    subnet,
    networkInterface,
    virtualMachine,
  } = useVirtualMachine();

  const handleSubmit = (event) => {
    var data = {
      infrastructureRequest: infrastructureRequest,
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
                .then((resp) => {
                  if (!resp.ok) {
                    let error = new Error("response was not ok");
                    error.response = resp;
                    error.status = resp.status;
                    throw error;
                  }
                  return resp;
                })
                .then((resp) => {
                  return resp.json();
                })
                .then((json) => {
                  alert("Virtual Machine Requested");
                })
                .catch((error) => {
                  error.response.text().then((text) => {
                    setError(text);
                    setShowError(true);
                  });
                });
            } catch (error) {
              setError(error);
              setShowError(true);
            }
          });
      }
    };

    postData();

    event.preventDefault();
  };

  return (
    <Container fluid>
      <h1>Request Virtual Machine</h1>

      <Toast
        className="d-inline-block m-1 float-right position-fixed top-0 end-0"
        bg="danger"
        show={showError}
        onClose={() => setShowError(false)}
        autohide={true}
        delay={5000}
      >
        <Toast.Header>
          <img src="holder.js/20x20?text=%20" className="rounded me-2" alt="" />
          <strong className="me-auto">Error</strong>
        </Toast.Header>
        <Toast.Body>{error}</Toast.Body>
      </Toast>

      <Form onSubmit={handleSubmit}>
        <FormRequester />
        <hr />
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
