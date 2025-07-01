from lxml import etree # type: ignore

def patch_urdf_limits(file_path):
    parser = etree.XMLParser(remove_blank_text=True)
    tree = etree.parse(file_path, parser)
    root = tree.getroot()

    for joint in root.findall(".//joint"):
        limit = joint.find("limit")
        if limit is not None:
            if "effort" not in limit.attrib:
                limit.set("effort", "100")
            if "velocity" not in limit.attrib:
                limit.set("velocity", "1.0")
            if "lower" not in limit.attrib:
                limit.set("lower", "0.0")
            if "upper" not in limit.attrib:
                limit.set("upper", "0.0")

    tree.write("Toilet/mobility_patched.urdf", pretty_print=True)
    print("Patched file saved as mobility_patched.urdf")

# Usage:
patch_urdf_limits("Toilet/mobility.urdf")
