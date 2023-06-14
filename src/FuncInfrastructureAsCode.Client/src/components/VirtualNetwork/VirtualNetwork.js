import React from 'react';
import './VirtualNetwork.css';
import Table from "react-bootstrap/Table";
import { useFetch } from '../../apis/FetchData';

const VirtualNetwork = () => {

  const subnets = useFetch("/VirtualNetworks");

  if (!Array.isArray(subnets.response)) {
    return <p>Loading...</p>;
  }

  const data = subnets.response;

  return (
    <div>
      <h3>VirtualNetworks</h3>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Localname</th>
            <th>Location</th>
            <th>ResourceGroupName</th>
            <th>AddressSpace</th>
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
                  <td>{row.addressSpace}</td>
                  <td>{row.status}</td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </div>
  );
}

VirtualNetwork.propTypes = {};

VirtualNetwork.defaultProps = {};

export default VirtualNetwork;
