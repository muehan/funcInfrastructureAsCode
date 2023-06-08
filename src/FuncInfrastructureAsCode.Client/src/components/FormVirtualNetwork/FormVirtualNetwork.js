import React from "react";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./FormVirtualNetwork.css";

const FormVirtualNetwork = () => {
  const {
    virtualNetwork,
    setVirtualNetworkName,
    setVirtualNetworkAddessSpace,
  } = useVirtualMachine();

  const handleVnNameChange = (event) => {
    setVirtualNetworkName(event.target.value);
  };

  const handleVnAddessSpaceChange = (event) => {
    setVirtualNetworkAddessSpace(event.target.value);
  };

  return (
    <>
      <h2>VirtualNetwork</h2>

      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.name">
            <Form.Label>Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="name"
              value={virtualNetwork.name}
              onChange={handleVnNameChange}
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.name">
            <Form.Label>AddressSpace</Form.Label>
            <Form.Control
              type="text"
              placeholder="name"
              value={virtualNetwork.addressSpace}
              onChange={handleVnAddessSpaceChange}
            />
          </Form.Group>
        </Col>
      </Row>

      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.name">
            <Form.Label>ResourceGroupName</Form.Label>
            <Form.Control
              type="text"
              placeholder="ResourceGroupName"
              value={virtualNetwork.resourceGroupName}
              disabled
            />
          </Form.Group>
        </Col>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.localName">
            <Form.Label>LocalName</Form.Label>
            <Form.Control
              type="text"
              placeholder="localName"
              value={virtualNetwork.localName}
              disabled
            />
          </Form.Group>
        </Col>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.location">
            <Form.Label>Location</Form.Label>
            <Form.Control
              type="text"
              placeholder="location"
              value={virtualNetwork.location}
              disabled
            />
          </Form.Group>
        </Col>
      </Row>
    </>
  );
};

FormVirtualNetwork.propTypes = {};

FormVirtualNetwork.defaultProps = {};

export default FormVirtualNetwork;
