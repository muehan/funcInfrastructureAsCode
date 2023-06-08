import React from "react";
import { createContext, useContext, useState } from "react";
import {
  NetworkInterface,
  ResourceGroup,
  Subnet,
  VirtualMachine,
  VirtualNetwork,
} from "../types";

interface VirtualMachineOutput {
  resourceGroup: ResourceGroup;
  virtualNetwork: VirtualNetwork;
  subnet: Subnet;
  networkInterface: NetworkInterface;
  virtualMachine: VirtualMachine;
  setResourceGroupName: (value: string) => void;
  setResourceGroupLocalName: (value: string) => void;
  setResourceGroupLocation: (value: string) => void;
  setVirtualNetworkName: (value: string) => void;
  setVirtualNetworkAddessSpace: (value: string) => void;
  setSubnetName: (value: string) => void;
  setSubnetAndressPrefixes: (value: string) => void;
}

const VirtualMachineContext = createContext<VirtualMachineOutput>({
  resourceGroup: {} as ResourceGroup,
  virtualNetwork: {} as VirtualNetwork,
  subnet: {} as Subnet,
  networkInterface: {} as NetworkInterface,
  virtualMachine: {} as VirtualMachine,
} as VirtualMachineOutput);

type Props = {
  children: string | JSX.Element | JSX.Element[];
};

const VirtualMachineProvider = ({ children }: Props) => {
  const [resourceGroup, setResourceGroup] = useState<ResourceGroup>({
    name: "",
    localName: "",
    location: "",
  });

  const [virtualNetwork, setvirtualNetwork] = useState<VirtualNetwork>({
    name: "",
    localName: "",
    location: "",
    resourceGroupName: "",
    addressSpace: "",
  });

  const [subnet, setSubnet] = useState<Subnet>({
    name: "",
    localName: "",
    resourceGroupName: "",
    addressPrefixes: "",
    virtualNetworkName: "",
  });

  const [networkInterface, setNetworkInterface] = useState<NetworkInterface>({
    name: "",
    localName: "",
    location: "",
    resourceGroupName: "",
    ipConfiguratioName: "",
  });

  const [virtualMachine, setVirtualMachine] = useState({
    name: "",
    localName: "",
    location: "",
    resourceGroupName: "",
    adminUsername: "",
    size: "",
    adminSshKeyPublicKey: "",
    adminSshKeyUsername: "",
    osDiskCachine: "",
    osDiskStorageAccountType: "",
    sourceImageReferenceOffer: "",
    sourceImageReferencePublisher: "",
    sourceImageReferenceSku: "",
    sourceImageReferenceVersion: "",
  });

  const handleResourceGroupNameChange = (value: string) => {
    setResourceGroup({ ...resourceGroup, name: value });
    setvirtualNetwork({ ...virtualNetwork, resourceGroupName: value });
    setSubnet({ ...subnet, resourceGroupName: value });
    setNetworkInterface({ ...networkInterface, resourceGroupName: value });
    setVirtualMachine({ ...virtualMachine, resourceGroupName: value });
  };

  const handleRgLocalNameChange = (value: string) => {
    setResourceGroup({ ...resourceGroup, localName: value });
    setvirtualNetwork({ ...virtualNetwork, localName: value });
    setSubnet({ ...subnet, localName: value });
    setNetworkInterface({ ...networkInterface, localName: value });
    setVirtualMachine({ ...virtualMachine, localName: value });
  };

  const handleRgLocationChange = (value: string) => {
    setResourceGroup({ ...resourceGroup, location: value });
    setvirtualNetwork({ ...virtualNetwork, location: value });
    setNetworkInterface({ ...networkInterface, location: value });
    setVirtualMachine({ ...virtualMachine, location: value });
  };

  const handleVnNameChange = (value: string) => {
    setvirtualNetwork({ ...virtualNetwork, name: value });
    setSubnet({ ...subnet, virtualNetworkName: value });
  };

  const handleVnAddessSpaceChange = (value: string) => {
    setvirtualNetwork({ ...virtualNetwork, location: value });
  };

  const handleSnNameChange = (value: string) => {
    setSubnet({ ...subnet, name: value });
  };

  const handleAndressPrefixesChange = (value: string) => {
    setSubnet({ ...subnet, addressPrefixes: value });
  };

  const handleNiNameChange = (value: string) => {
    setNetworkInterface({ ...networkInterface, name: value });
  };

  const handleNiIpConfiguratioNameChange = (value: string) => {
    setNetworkInterface({
      ...networkInterface,
      ipConfiguratioName: value,
    });
  };

  const handleVmNameChange = (value: string) => {
    setVirtualMachine({ ...virtualMachine, name: value });
  };

  const handleVmAdminUsernameChange = (value: string) => {
    setVirtualMachine({ ...virtualMachine, adminUsername: value });
  };

  const handleVmSizeChange = (value: string) => {
    setVirtualMachine({ ...virtualMachine, size: value });
  };

  const handleVmAdminSshKeyUsernameChange = (value: string) => {
    setVirtualMachine({
      ...virtualMachine,
      adminSshKeyUsername: value,
    });
  };

  const handleVmAdminSshKeyPublicKeyChange = (value: string) => {
    setVirtualMachine({
      ...virtualMachine,
      adminSshKeyPublicKey: value,
    });
  };

  const handleVmOsDiskCachineChange = (value: string) => {
    setVirtualMachine({ ...virtualMachine, osDiskCachine: value });
  };

  const handleVmOsDiskStorageAccountTypeChange = (value: string) => {
    setVirtualMachine({
      ...virtualMachine,
      osDiskStorageAccountType: value,
    });
  };

  const handleVmSourceImageReferenceOfferChange = (value: string) => {
    setVirtualMachine({
      ...virtualMachine,
      sourceImageReferenceOffer: value,
    });
  };

  const handleVmSourceImageReferencePublisherChange = (value: string) => {
    setVirtualMachine({
      ...virtualMachine,
      sourceImageReferencePublisher: value,
    });
  };

  const handleVmSourceImageReferenceSkuChange = (value: string) => {
    setVirtualMachine({
      ...virtualMachine,
      sourceImageReferenceSku: value,
    });
  };

  const handleVmSourceImageReferenceVersionChange = (value: string) => {
    setVirtualMachine({
      ...virtualMachine,
      sourceImageReferenceVersion: value,
    });
  };

  const actions = {
    setResourceGroupName: handleResourceGroupNameChange,
    setResourceGroupLocalName: handleRgLocalNameChange,
    setResourceGroupLocation: handleRgLocationChange,
    setVirtualNetworkName: handleVnNameChange,
    setVirtualNetworkAddessSpace: handleVnAddessSpaceChange,
    setSubnetName: handleSnNameChange,
    setSubnetAndressPrefixes: handleAndressPrefixesChange,
    setNetworkInterfaceName: handleNiNameChange,
    setNetworkInterfaceIpConfiguratioName: handleNiIpConfiguratioNameChange,
    setVirtualMachineName: handleVmNameChange,
    setVirtualMachineAdminUsername: handleVmAdminUsernameChange,
    setVirtualMachineSize: handleVmSizeChange,
    setVirtualMachineAdminSshKeyUsername: handleVmAdminSshKeyUsernameChange,
    setVirtualMachineAdminSshKeyPublicKey: handleVmAdminSshKeyPublicKeyChange,
    setVirtualMachineOsDiskCachine: handleVmOsDiskCachineChange,
    setVirtualMachineOsDiskStorageAccountType:
      handleVmOsDiskStorageAccountTypeChange,
    setVirtualMachineSourceImageReferenceOffer:
      handleVmSourceImageReferenceOfferChange,
    setVirtualMachineSourceImageReferencePublisher:
      handleVmSourceImageReferencePublisherChange,
    setVirtualMachineSourceImageReferenceSku:
      handleVmSourceImageReferenceSkuChange,
    setVirtualMachineSourceImageReferenceVersion:
      handleVmSourceImageReferenceVersionChange,
  };

  return (
    <VirtualMachineContext.Provider
      value={{
        resourceGroup,
        virtualNetwork,
        subnet,
        networkInterface,
        virtualMachine,
        ...actions,
      }}
    >
      {children}
    </VirtualMachineContext.Provider>
  );
};

export default VirtualMachineProvider;

export const useVirtualMachine = () => useContext(VirtualMachineContext);
