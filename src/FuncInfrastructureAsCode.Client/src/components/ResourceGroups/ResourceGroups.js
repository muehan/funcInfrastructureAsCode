import "./ResourceGroups.css";
import React from "react";
import Table from "react-bootstrap/Table";
import { useResourceGroups } from "./../../helpers/FetchResourceGroups";

const ResourceGroups = () => {
  const resourceGroups = useResourceGroups();

  if (!Array.isArray(resourceGroups.response)) {
    return <p>Loading...</p>;
  }

  const data = resourceGroups.response;

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Name</th>
          <th>Localname</th>
          <th>Location</th>
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
              </tr>
            )
          })}
      </tbody>
    </Table>
  );
};

ResourceGroups.propTypes = {};

ResourceGroups.defaultProps = {};

export default ResourceGroups;
