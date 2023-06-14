export interface InfrastructureRequest {
  id: string | null;
  requesterName: string;
  requesterEmail: string;
  requestStatus: string;
  createdAt: Date;
}

export interface ResourceGroup {
  name: string;
  localName: string;
  location: string;
}

export interface VirtualNetwork {
  name: string;
  localName: string;
  location: string;
  resourceGroupName: string;
  addressSpace: string;
}

export interface Subnet {
  name: string;
  localName: string;
  resourceGroupName: string;
  virtualNetworkName: string;
  addressPrefixes: string;
}

export interface NetworkInterface {
  name: string;
  localName: string;
  location: string;
  resourceGroupName: string;
  ipConfiguratioName: string;
  ipConfiguratioPrivateIpAddressAllocation: string;
}

export interface VirtualMachine {
  name: string;
  localName: string;
  location: string;
  resourceGroupName: string;
  adminUsername: string;
  size: string;
  adminSshKeyPublicKey: string;
  adminSshKeyUsername: string;
  osDiskCachine: string;
  osDiskStorageAccountType: string;
  sourceImageReferenceOffer: string;
  sourceImageReferencePublisher: string;
  sourceImageReferenceSku: string;
  sourceImageReferenceVersion: string;
}
