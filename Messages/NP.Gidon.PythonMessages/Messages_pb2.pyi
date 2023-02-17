from google.protobuf.internal import enum_type_wrapper as _enum_type_wrapper
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Optional as _Optional

DESCRIPTOR: _descriptor.FileDescriptor
None: Topic
WindowInfoTopic: Topic

class WindowInfo(_message.Message):
    __slots__ = ["UniqueWindowId", "WindowHandle"]
    UNIQUEWINDOWID_FIELD_NUMBER: _ClassVar[int]
    UniqueWindowId: str
    WINDOWHANDLE_FIELD_NUMBER: _ClassVar[int]
    WindowHandle: int
    def __init__(self, UniqueWindowId: _Optional[str] = ..., WindowHandle: _Optional[int] = ...) -> None: ...

class Topic(int, metaclass=_enum_type_wrapper.EnumTypeWrapper):
    __slots__ = []
