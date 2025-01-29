import { StyleSheet, Text, View, Dimensions, TouchableOpacity } from 'react-native'
import React from 'react'
import ProfileSvg from '../Svgs/ProfileSvg'
import { ACTIVE_OPACITY } from '../../utils/constants'
import { useNavigation } from '@react-navigation/native'

const Header = () => {
    const navigate = useNavigation()
    return (
        <View style={styles.header}>
            <Text style={styles.text}>ChatBoard</Text>

            <TouchableOpacity
                hitSlop={{ bottom: 20, left: 20, right: 20, top: 20 }}
                activeOpacity={ACTIVE_OPACITY} onPress={() => {
                    navigate.navigate("Profile")
                }}>
                <ProfileSvg />
            </TouchableOpacity>
        </View>
    )
}

export default Header

const styles = StyleSheet.create({
    header: {
        height: 110,
        width: '100%', // Takes the full width of the screen
        backgroundColor: "#ECE2E1",
        flexDirection: "row",
        justifyContent: "space-between",
        alignItems: "flex-end",
        paddingBottom:20
    },
    text: {
        fontSize: 25,
        fontFamily: "Ubuntu-Bold",
        paddingLeft: 20
    }
})
