import React from "react";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./FormNetworkInterface.css";

const FormNetworkInterface = () => {

  const {
    networkInterface,
    setNetworkInterfaceName,
    setNetworkInterfaceIpConfiguratioName,
    setIpConfiguratioPrivateIpAddressAllocation
  } = useVirtualMachine();

  const handleNiNameChange = (event) => {
    setNetworkInterfaceName(event.target.value);
  };

  const handleNiIpConfiguratioNameChange = (event) => {
    setNetworkInterfaceIpConfiguratioName(event.target.value);
  };

  const handleNiIpConfiguratioPrivateIpAddressAllocation = (event) => {
    setIpConfiguratioPrivateIpAddressAllocation(event.target.value);
  };

  return (
    <>
      <h2>NetworkInterface</h2>

      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.ni.name">
            <Form.Label>Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="name"
              value={networkInterface.name}
              onChange={handleNiNameChange}
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.ni.ipconfigurationname">
            <Form.Label>IpConfiguratioName</Form.Label>
            <Form.Control
              type="text"
              placeholder="IpConfiguratioName"
              value={networkInterface.ipConfiguratioName}
              onChange={handleNiIpConfiguratioNameChange}
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.ni.ipconfiguratioprivateipaddressallocation">
            <Form.Label>IpConfiguratioPrivateIpAddressAllocation</Form.Label>
            <Form.Control
              type="text"
              placeholder="IpConfiguratioPrivateIpAddressAllocation"
              value={networkInterface.ipConfiguratioPrivateIpAddressAllocation}
              onChange={handleNiIpConfiguratioPrivateIpAddressAllocation}
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
              value={networkInterface.resourceGroupName}
              disabled
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.ni.localName">
            <Form.Label>LocalName</Form.Label>
            <Form.Control
              type="text"
              placeholder="localName"
              value={networkInterface.localName}
              disabled
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.ni.location">
            <Form.Label>Location</Form.Label>
            <Form.Control
              type="text"
              placeholder="location"
              value={networkInterface.location}
              disabled
            />
          </Form.Group>
        </Col>
      </Row>
    </>
  );
};

FormNetworkInterface.propTypes = {};

FormNetworkInterface.defaultProps = {};

export default FormNetworkInterface;
