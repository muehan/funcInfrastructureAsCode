import "./NetworkInterfaces.css";
import React from "react";
import Table from "react-bootstrap/Table";
import { useFetch } from "../../apis/FetchData";

const NetworkInterfaces = () => {
  const networkInterfaces = useFetch("/NetworkInterfaces");

  if (!Array.isArray(networkInterfaces.response)) {
    return <p>Loading...</p>;
  }

  const data = networkInterfaces.response;

  return (
    <div>
      <h3>NetworkInterfaces</h3>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Localname</th>
            <th>Location</th>
            <th>ResourceGroupName</th>
            <th>IpConfiguratioName</th>
            <th>IpConfiguratioPrivateIpAddressAllocation</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((row) => {
              return (
                <tr key={row.name}>
                  <td>{row.name}</td>
                  <td>{row.localName}</td>
                  <td>{row.location}</td>
                  <td>{row.resourceGroupName}</td>
                  <td>{row.ipConfiguratioName}</td>
                  <td>{row.ipConfiguratioPrivateIpAddressAllocation}</td>
                  <td>{row.status}</td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </div>
  );
};

NetworkInterfaces.propTypes = {};

NetworkInterfaces.defaultProps = {};

export default NetworkInterfaces;
