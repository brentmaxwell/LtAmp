using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model
{
    public static class MessageFactory
    {
        public static FenderMessageLT Create(IMessage message)
        {
            FenderMessageLT fenderMessage = new FenderMessageLT() { ResponseType = ResponseType.Unsolicited };
            switch (message.GetType().Name)
            {
                case "IndexPot":
                    fenderMessage.IndexPot = (IndexPot)message;
                    break;
                case "IndexButton":
                    fenderMessage.IndexButton = (IndexButton)message;
                    break;
                case "IndexEncoder":
                    fenderMessage.IndexEncoder = (IndexEncoder)message;
                    break;
                case "ActiveDisplay":
                    fenderMessage.ActiveDisplay = (ActiveDisplay)message;
                    break;
                case "ProcessorUtilizationRequest":
                    fenderMessage.ProcessorUtilizationRequest = (ProcessorUtilizationRequest)message;
                    break;
                case "ProcessorUtilization":
                    fenderMessage.ProcessorUtilization = (ProcessorUtilization)message;
                    break;
                case "MemoryUsageRequest":
                    fenderMessage.MemoryUsageRequest = (MemoryUsageRequest)message;
                    break;
                case "MemoryUsageStatus":
                    fenderMessage.MemoryUsageStatus = (MemoryUsageStatus)message;
                    break;
                case "PresetJSONMessageRequestLT":
                    fenderMessage.PresetJSONMessageRequestLT = (PresetJSONMessageRequest_LT)message;
                    break;
                case "FrameBufferMessageRequest":
                    fenderMessage.FrameBufferMessageRequest = (FrameBufferMessageRequest)message;
                    break;
                case "FrameBufferMessage":
                    fenderMessage.FrameBufferMessage = (FrameBufferMessage)message;
                    break;
                case "Lt4FootswitchModeRequest":
                    fenderMessage.Lt4FootswitchModeRequest = (LT4FootswitchModeRequest)message;
                    break;
                case "Lt4FootswitchModeStatus":
                    fenderMessage.Lt4FootswitchModeStatus = (LT4FootswitchModeStatus)message;
                    break;
                case "LoadPresetTestSuite":
                    fenderMessage.LoadPresetTestSuite = (LoadPreset_TestSuite)message;
                    break;
                case "LoopbackTest":
                    fenderMessage.LoopbackTest = (LoopbackTest)message;
                    break;
                case "PresetJSONMessage":
                    fenderMessage.PresetJSONMessage = (PresetJSONMessage)message;
                    break;
                case "CurrentPresetStatus":
                    fenderMessage.CurrentPresetStatus = (CurrentPresetStatus)message;
                    break;
                case "LoadPreset":
                    fenderMessage.LoadPreset = (LoadPreset)message;
                    break;
                case "SetDspUnitParameter":
                    fenderMessage.SetDspUnitParameter = (SetDspUnitParameter)message;
                    break;
                case "SetDspUnitParameterStatus":
                    fenderMessage.SetDspUnitParameterStatus = (SetDspUnitParameterStatus)message;
                    break;
                case "DspUnitParameterStatus":
                    fenderMessage.DspUnitParameterStatus = (DspUnitParameterStatus)message;
                    break;
                case "CurrentLoadedPresetIndexStatus":
                    fenderMessage.CurrentLoadedPresetIndexStatus = (CurrentLoadedPresetIndexStatus)message;
                    break;
                case "PresetEditedStatus":
                    fenderMessage.PresetEditedStatus = (PresetEditedStatus)message;
                    break;
                case "ReplaceNode":
                    fenderMessage.ReplaceNode = (ReplaceNode)message;
                    break;
                case "ReplaceNodeStatus":
                    fenderMessage.ReplaceNodeStatus = (ReplaceNodeStatus)message;
                    break;
                case "ShiftPreset":
                    fenderMessage.ShiftPreset = (ShiftPreset)message;
                    break;
                case "ShiftPresetStatus":
                    fenderMessage.ShiftPresetStatus = (ShiftPresetStatus)message;
                    break;
                case "SwapPreset":
                    fenderMessage.SwapPreset = (SwapPreset)message;
                    break;
                case "SwapPresetStatus":
                    fenderMessage.SwapPresetStatus = (SwapPresetStatus)message;
                    break;
                case "CurrentPresetSet":
                    fenderMessage.CurrentPresetSet = (CurrentPresetSet)message;
                    break;
                case "CurrentLoadedPresetIndexBypassStatus":
                    fenderMessage.CurrentLoadedPresetIndexBypassStatus = (CurrentLoadedPresetIndexBypassStatus)message;
                    break;
                case "CurrentDisplayedPresetIndexStatus":
                    fenderMessage.CurrentDisplayedPresetIndexStatus = (CurrentDisplayedPresetIndexStatus)message;
                    break;
                case "PresetSavedStatus":
                    fenderMessage.PresetSavedStatus = (PresetSavedStatus)message;
                    break;
                case "ClearPreset":
                    fenderMessage.ClearPreset = (ClearPreset)message;
                    break;
                case "ClearPresetStatus":
                    fenderMessage.ClearPresetStatus = (ClearPresetStatus)message;
                    break;
                case "SaveCurrentPreset":
                    fenderMessage.SaveCurrentPreset = (SaveCurrentPreset)message;
                    break;
                case "SaveCurrentPresetTo":
                    fenderMessage.SaveCurrentPresetTo = (SaveCurrentPresetTo)message;
                    break;
                case "SavePresetAs":
                    fenderMessage.SavePresetAs = (SavePresetAs)message;
                    break;
                case "NewPresetSavedStatus":
                    fenderMessage.NewPresetSavedStatus = (NewPresetSavedStatus)message;
                    break;
                case "RenamePresetAt":
                    fenderMessage.RenamePresetAt = (RenamePresetAt)message;
                    break;
                case "AuditionPreset":
                    fenderMessage.AuditionPreset = (AuditionPreset)message;
                    break;
                case "AuditionPresetStatus":
                    fenderMessage.AuditionPresetStatus = (AuditionPresetStatus)message;
                    break;
                case "ExitAuditionPreset":
                    fenderMessage.ExitAuditionPreset = (ExitAuditionPreset)message;
                    break;
                case "ExitAuditionPresetStatus":
                    fenderMessage.ExitAuditionPresetStatus = (ExitAuditionPresetStatus)message;
                    break;
                case "AuditionStateRequest":
                    fenderMessage.AuditionStateRequest = (AuditionStateRequest)message;
                    break;
                case "AuditionStateStatus":
                    fenderMessage.AuditionStateStatus = (AuditionStateStatus)message;
                    break;
                case "ProductIdentificationStatus":
                    fenderMessage.ProductIdentificationStatus = (ProductIdentificationStatus)message;
                    break;
                case "ProductIdentificationRequest":
                    fenderMessage.ProductIdentificationRequest = (ProductIdentificationRequest)message;
                    break;
                case "FirmwareVersionRequest":
                    fenderMessage.FirmwareVersionRequest = (FirmwareVersionRequest)message;
                    break;
                case "FirmwareVersionStatus":
                    fenderMessage.FirmwareVersionStatus = (FirmwareVersionStatus)message;
                    break;
                case "CurrentPresetRequest":
                    fenderMessage.CurrentPresetRequest = (CurrentPresetRequest)message;
                    break;
                case "RetrievePreset":
                    fenderMessage.RetrievePreset = (RetrievePreset)message;
                    break;
                case "UsbGainRequest":
                    fenderMessage.UsbGainRequest = (UsbGainRequest)message;
                    break;
                case "UsbGainStatus":
                    fenderMessage.UsbGainStatus = (UsbGainStatus)message;
                    break;
                case "QASlotsRequest":
                    fenderMessage.QASlotsRequest = (QASlotsRequest)message;
                    break;
                case "QASlotsStatus":
                    fenderMessage.QASlotsStatus = (QASlotsStatus)message;
                    break;
                case "LineOutGainRequest":
                    fenderMessage.LineOutGainRequest = (LineOutGainRequest)message;
                    break;
                case "LineOutGainStatus":
                    fenderMessage.LineOutGainStatus = (LineOutGainStatus)message;
                    break;
                case "ModalStatusMessage":
                    fenderMessage.ModalStatusMessage = (ModalStatusMessage)message;
                    break;
                case "UsbGainSet":
                    fenderMessage.UsbGainSet = (UsbGainSet)message;
                    break;
                case "LineOutGainSet":
                    fenderMessage.LineOutGainSet = (LineOutGainSet)message;
                    break;
                case "QASlotsSet":
                    fenderMessage.QASlotsSet = (QASlotsSet)message;
                    break;
                case "UnsupportedMessageStatus":
                    fenderMessage.UnsupportedMessageStatus = (UnsupportedMessageStatus)message;
                    break;
                case "Heartbeat":
                    fenderMessage.Heartbeat = (Heartbeat)message;
                    break;
                case "ConnectionStatusRequest":
                    fenderMessage.ConnectionStatusRequest = (ConnectionStatusRequest)message;
                    break;
                case "ConnectionStatus":
                    fenderMessage.ConnectionStatus = (ConnectionStatus)message;
                    break;
                default:
                    fenderMessage.UnsupportedMessageStatus = new UnsupportedMessageStatus() { Status = ErrorType.Unsupported };
                    break;
            }
            return fenderMessage;
        }
    }
}
