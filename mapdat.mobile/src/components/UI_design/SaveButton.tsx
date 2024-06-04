import { TouchableOpacity } from "react-native"
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { faFloppyDisk } from "@fortawesome/free-solid-svg-icons"
import { SaveButtonProps } from "../../models/interfaces/SaveButton.interfaces"

export const SaveButton: React.FC<SaveButtonProps> = ({ onPress, disabled = true }) => {
    return (
        <>
            {
                disabled &&
                <TouchableOpacity onPress={onPress}>
                    <FontAwesomeIcon
                        icon={faFloppyDisk}
                        color="white"
                        size={25} />
                </TouchableOpacity>
            }
        </>
    )
}